using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Users.Queries;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Application.Account.Queries.GetCurrentUser
{
    internal class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, UserDto>
    {
        private readonly ICurrentUser _currentUser;
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCurrentUserQueryHandler(ICurrentUser currentUser,
            IApplicationDbContext context, 
            IMapper mapper)
        {
            _currentUser = currentUser;
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var user =  await _context.Users
                .Include(u => u.Doctor)
                .ThenInclude(d => d.Speciality)
                .Include(u => u.Doctor)
                .ThenInclude(d => d.Languages)
                .FirstOrDefaultAsync(u=>u.Id == _currentUser.Id);

            if (user == null)
                throw new NotFoundException(nameof(User));
            
            return _mapper.Map<UserDto>(user);
        }
    }
}
