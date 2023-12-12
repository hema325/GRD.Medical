using Application.Account.Commands;
using Application.Common.Exceptions;
using Application.Common.Models.EmailTemplates;
using Infrastructure.Authentication.JWT.JWTManager;
using Infrastructure.Authentication.PasswordHasher;
using Infrastructure.Authentication.Settings;
using Infrastructure.Email.EmailTemplateModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Web;

namespace Infrastructure.Authentication
{
    internal class AuthenticationService: IAuthentication
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJWTManager _jwtManager;
        private readonly IApplicationDbContext _context;
        private readonly RefreshTokenSettings _refreshTokenSettings;
        private readonly IEmailSender _emailSender;
        private readonly IDistributedCache _distributedCache;
        private readonly ClientSettings _clientSettins;
        private readonly ICurrentUser _currentUser;
        private readonly ICurrentHttpRequest _httpRequest;

        public AuthenticationService(IPasswordHasher passwordHasher,
            IJWTManager jwtManager,
            IApplicationDbContext context,
            IOptions<RefreshTokenSettings> refreshTokenSettings,
            IEmailSender emailSender,
            IDistributedCache distributedCache,
            IOptions<ClientSettings> clientSettings,
            ICurrentUser currentUser,
            ICurrentHttpRequest httpRequest)
        {
            _passwordHasher = passwordHasher;
            _jwtManager = jwtManager;
            _context = context;
            _refreshTokenSettings = refreshTokenSettings.Value;
            _emailSender = emailSender;
            _distributedCache = distributedCache;
            _clientSettins = clientSettings.Value;
            _currentUser = currentUser;
            _httpRequest = httpRequest;
        }

        public async Task RegisterAsync(User user, string password)
        {
            user.HashedPassword = _passwordHasher.HashPassword(password);
            user.AddDomainEvent(new EntityCreatedEvent(user));
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            SendEmailConfirmationAsync(user.Email);
        }

        public async Task<AuthResultDto> AuthenticateAsync(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null || !_passwordHasher.VerifyHashedPassword(user.HashedPassword, password))
                throw new UnauthorizedException("Email or password is not correct");

            if (!user.IsEmailConfirmed)
                throw new UnauthorizedException("Email is not confirmed");

            return await GetAuthResultDtoAsync(user);
        }

        public async Task<AuthResultDto> RequestJwtTokenAsync(string rfToken)
        {
            var refreshToken = await _context.RefreshTokens
                .Include(rt => rt.User)
                .FirstOrDefaultAsync(rt => rt.Token == rfToken);

            if(refreshToken != null)
                await RemoveRefreshTokenAsync(refreshToken);

            if (refreshToken == null || DateTime.UtcNow>=refreshToken.ExpiresOn)
                throw new UnauthorizedException("Invalid token");

            return await GetAuthResultDtoAsync(refreshToken.User);
        }

        public async Task RevokeRefreshTokenAsync(string rfToken)
        {
            var refreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == rfToken);

            if (refreshToken == null)
                throw new UnauthorizedException("Invalid token");

            await RemoveRefreshTokenAsync(refreshToken);
        }

        private async Task<AuthResultDto> GetAuthResultDtoAsync(User user)
        {
            var jwtToken = _jwtManager.GenerateToken(user);
            var refreshToken = await CreateRefreshTokenAsync(user.Id);

            return new AuthResultDto
            {
                Id = user.Id,
                Name = $"{user.FirstName} {user.LastName}",
                Email = user.Email,
                Role = user.Role.ToString(),
                ImageUrl = user.ImageUrl== null ? null:$"{_httpRequest.Scheme}://{_httpRequest.Host}/{user.ImageUrl}",
                JWTToken = jwtToken.Token,
                JWTTokenExpiresOn = jwtToken.ExpiresOn,
                RefreshToken = refreshToken.Token,
                RefreshTokenExpiresOn = refreshToken.ExpiresOn
            };
        }

        private async Task<RefreshToken> CreateRefreshTokenAsync(int userId)
        {
            var bytes = new byte[_refreshTokenSettings.Length];
            RandomNumberGenerator.Create().GetBytes(bytes);

            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(bytes),
                ExpiresOn = DateTime.UtcNow.AddDays(_refreshTokenSettings.ExpirationInDays),
                UserId = userId
            };

            _context.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync();

            return refreshToken;
        }

        private async Task RemoveRefreshTokenAsync(RefreshToken refreshToken)
        {
            _context.RefreshTokens.Remove(refreshToken);
            await _context.SaveChangesAsync();
        }

        public async Task SendEmailConfirmationAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
                throw new UnauthorizedException("Invalid email address.");

            if (user.IsEmailConfirmed)
                throw new ConflictException("Email already confirmed.");

            //generate email confirmation model & cache it
            var key = $"emailConfirmationToken-{user.Id}";
            var token = Guid.NewGuid().ToString();
            await _distributedCache.SetStringAsync(key, token,new DistributedCacheEntryOptions() { 
                AbsoluteExpiration = DateTime.UtcNow.AddHours(1)
            });

            //build email confirmation model
            var emailConfirmation = new EmailConfirmationTemplate
            {
                ConfirmationUrl = $"{_clientSettins.BaseUrl}/account/confirm-email?userId={user.Id}&token={HttpUtility.UrlEncode(token)}"
            };

            await _emailSender.SendEmailConfirmationAsync(user.Email, emailConfirmation);
        }

        public async Task<AuthResultDto> ConfirmEmailAsync(int userId, string token)
        {
            //fetch stored token
            var key = $"emailConfirmationToken-{userId}";
            var confimrationToken = await _distributedCache.GetStringAsync(key);

            //check if user has this token
            if (confimrationToken == null || confimrationToken != token)
                throw new UnauthorizedException("Failed to confirm your email address.");

            //confirm user email
            var user = await _context.Users.FindAsync(userId);
            user.IsEmailConfirmed = true;
            await _context.SaveChangesAsync();

            await _distributedCache.RemoveAsync(key);
            return await GetAuthResultDtoAsync(user);
        }

        public async Task SendEmailResetPasswordAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
                throw new UnauthorizedException("Invalid email address.");

            //generate email reset password model & cache it
            var key = $"emailResetPasswordToken-{user.Id}";
            var token = Guid.NewGuid().ToString();
            await _distributedCache.SetStringAsync(key, token,new DistributedCacheEntryOptions() { 
                AbsoluteExpiration = DateTime.UtcNow.AddHours(1)
            });

            //build email reset password model
            var emailResetPassword = new EmailResetPasswordTemplate
            {
                ResetUrl = $"{_clientSettins.BaseUrl}/account/reset-password?userId={user.Id}&token={HttpUtility.UrlEncode(token)}"
            };

            await _emailSender.SendEmailResetPasswordAsync(user.Email, emailResetPassword);
        }

        public async Task ResetPasswordAsync(int userId, string token, string newPassword)
        {
            //fetch stored token
            var key = $"emailResetPasswordToken-{userId}";
            var resatingToken = await _distributedCache.GetStringAsync(key);

            //check if user has this token
            if (resatingToken == null || resatingToken != token)
                throw new UnauthorizedException("Failed to reset your password.");

            //reset user password
            var user = await _context.Users.FindAsync(userId);
            user!.HashedPassword = _passwordHasher.HashPassword(newPassword);
            await _context.SaveChangesAsync();

            await _distributedCache.RemoveAsync(key);
        }

        public async Task ChangePasswordAsync(string oldPassword, string newPassword)
        {
            var userId = _currentUser.Id;
            var user = await _context.Users.FindAsync(userId);

            if (user == null || !_passwordHasher.VerifyHashedPassword(user.HashedPassword, oldPassword))
                throw new UnauthorizedException("Old password isn't correct");

            user.HashedPassword = _passwordHasher.HashPassword(newPassword);
            await _context.SaveChangesAsync();
        }

    }
}
