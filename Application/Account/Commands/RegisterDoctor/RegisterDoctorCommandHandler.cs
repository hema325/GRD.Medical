using Microsoft.EntityFrameworkCore;

namespace Application.Account.Commands.RegisterDoctor
{
    internal class RegisterDoctorCommandHandler: IRequestHandler<RegisterDoctorCommand>
    {
        private readonly IAuthentication _auth;
        private readonly IApplicationDbContext _context;

        public RegisterDoctorCommandHandler(IAuthentication auth, IApplicationDbContext context)
        {
            _auth = auth;
            _context = context;
        }

        public async Task<Unit> Handle(RegisterDoctorCommand request, CancellationToken cancellationToken)
        {
            var languages = await _context.Languages
                .Where(l => request.LanguageIds.Any(i => i == l.Id))
                .ToListAsync();
            
            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                IsEmailConfirmed = false,
                JoinedOn = DateTime.UtcNow,
                Role = Roles.Doctor,
                Doctor = new Doctor
                {
                    Biography = request.Biography,
                    ConsultFee = request.ConsultFee,
                    SpecialityId = request.SpecialityId,
                    Languages = languages,
                    Experience = request.Experience
                }
            };

            await _auth.RegisterAsync(user, request.Password);

            return Unit.Value;
        }
    }
}
