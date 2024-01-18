using Microsoft.EntityFrameworkCore;

namespace Application.Notifications.Queries.GetUnReadNotificationCount
{
    internal class GetUnReadNotificationCountQueryHandler : IRequestHandler<GetUnReadNotificationCountQuery, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUser _currentUser;

        public GetUnReadNotificationCountQueryHandler(IApplicationDbContext context, ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public Task<int> Handle(GetUnReadNotificationCountQuery request, CancellationToken cancellationToken)
        {
            return _context.Notifications
                .Where(n => !n.IsRead && n.OwnerId == _currentUser.Id)
                .CountAsync();
        }
    }
}
