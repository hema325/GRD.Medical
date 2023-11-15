using Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Authentication.JWT.JWTManager
{
    internal class JWTManager : IJWTManager
    {
        private readonly JWTSettings _jwtSettings;

        public JWTManager(IOptions<JWTSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public JWTToken GenerateToken(User user)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings?.Key!));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings?.Issuer,
                claims: ExtractUserClaims(user),
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings!.ExpirationInMinutes),
                signingCredentials: signingCredentials);

            return new JWTToken
            {
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                ExpiresOn = jwtSecurityToken.ValidTo
            };
        }

        private Claim[] ExtractUserClaims(User user)
        {
            return new[] {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };
        }
    }
}
