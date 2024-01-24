using Microsoft.EntityFrameworkCore;

namespace Application.Account.Commands.UpdateDoctor
{
    internal class UpdateDoctorCommandHandler: IRequestHandler<UpdateDoctorCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUser _currentUser;

        public UpdateDoctorCommandHandler(IApplicationDbContext context, ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<Unit> Handle(UpdateDoctorCommand request, CancellationToken cancellationToken)
        {
            var languages = await _context.Languages
                .Where(l => request.LanguageIds.Any(i => i == l.Id))
                .ToListAsync();

            var user = await _context.Users
                .Include(u => u.Doctor)
                .ThenInclude(d => d.Speciality)
                .Include(u => u.Doctor)
                .ThenInclude(d => d.Languages)
                .FirstOrDefaultAsync(u => u.Id == _currentUser.Id);

            if (user == null)
                throw new NotFoundException(nameof(User));

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Doctor.Biography = request.Biography;
            user.Doctor.ConsultFee = request.ConsultFee;
            user.Doctor.SpecialityId = request.SpecialityId;
            user.Doctor.Languages = languages;
            user.Doctor.Experience = request.Experience;

            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
