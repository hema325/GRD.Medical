using Application.Common.Extensions;
using Application.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Notifications.Queries.GetNotifications
{
    internal class GetNotificationsQueryHandler : IRequestHandler<GetNotificationsQuery, PaginatedList<NotificationDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUser _currentUser;
        private readonly IMapper _mapper;

        public GetNotificationsQueryHandler(IApplicationDbContext context, ICurrentUser currentUser, IMapper mapper)
        {
            _context = context;
            _currentUser = currentUser;
            _mapper = mapper;
        }

        public async Task<PaginatedList<NotificationDto>> Handle(GetNotificationsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Notifications
                .OrderByDescending(n => n.NotifiedOn)
                .Where(n => n.OwnerId == _currentUser.Id)
                .Include(n=>n.Initiator)
                .AsQueryable();

            if (request.Before != null)
                query = query.Where(n => n.NotifiedOn < request.Before);

            var notifications = await query.PaginateAsync(request.PageNumber, request.PageSize);

            return _mapper.Map<PaginatedList<NotificationDto>>(notifications);
        }
    }
}
