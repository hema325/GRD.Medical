using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Account.Queries.CheckEmailDuplication
{
    internal class CheckEmailDuplicationCommandHandler : IRequestHandler<CheckEmailDuplicationCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public CheckEmailDuplicationCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(CheckEmailDuplicationCommand request, CancellationToken cancellationToken)
        {
            return await _context.Users.AnyAsync(u => u.Email == request.Email);
        }
    }
}
