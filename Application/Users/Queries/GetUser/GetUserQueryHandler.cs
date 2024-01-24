using Application.Users.Queries;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Queries.GetUser
{
    internal class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetUserQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .Include(u => u.Doctor)
                .ThenInclude(d => d.Speciality)
                .Include(u => u.Doctor)
                .ThenInclude(d => d.Languages)
                .FirstOrDefaultAsync(u => u.Id == request.Id);

            if (user == null)
                throw new NotFoundException(nameof(User));

            return _mapper.Map<UserDto>(user);
        }
    }
}
