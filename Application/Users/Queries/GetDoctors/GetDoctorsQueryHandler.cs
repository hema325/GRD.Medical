using Application.Common.Extensions;
using Application.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Queries.GetDoctors
{
    internal class GetDoctorsQueryHandler : IRequestHandler<GetDoctorsQuery, PaginatedList<UserDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetDoctorsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<UserDto>> Handle(GetDoctorsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Users
                .Include(u => u.Doctor)
                .ThenInclude(d => d.Speciality)
                .Include(u => u.Doctor)
                .ThenInclude(d => d.Languages)
                .Where(u=>u.Role == Roles.Doctor && u.IsEmailConfirmed)
                .AsQueryable();

            if (request.Name != null)
                query = query.Where(u => (u.FirstName + " " + u.LastName).StartsWith(request.Name));
            
            if(request.Experience != null)
                query = query.Where(u=>u.Doctor.Experience == request.Experience);

            if (request.SpecialityId != null)
                query = query.Where(u => u.Doctor.SpecialityId == request.SpecialityId);

            switch (request.OrderBy)
            {
                case DoctorFilterConstants.FeeAsc:
                    query = query.OrderBy(u => u.Doctor.ConsultFee);
                    break;
                case DoctorFilterConstants.FeeDesc:
                    query = query.OrderByDescending(u => u.Doctor.ConsultFee);
                    break;
                case DoctorFilterConstants.ExperAsc:
                    query = query.OrderBy(u => u.Doctor.Experience);
                    break;
                case DoctorFilterConstants.ExperDesc:
                    query = query.OrderByDescending(u => u.Doctor.Experience);
                    break;
                default:
                    query = query.OrderBy(u => u.Id);
                    break;
            }

            var doctors = await query.PaginateAsync(request.PageNumber, request.PageSize);

            return _mapper.Map<PaginatedList<UserDto>>(doctors);

        }
    }
}
