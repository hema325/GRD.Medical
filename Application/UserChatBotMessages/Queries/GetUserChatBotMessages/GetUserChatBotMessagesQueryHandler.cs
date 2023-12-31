﻿using Application.Common.Extensions;
using Application.Common.Models;
using AutoMapper.QueryableExtensions;

namespace Application.UserChatBotMessages.Queries.GetUserChatBotMessages
{
    internal class GetUserChatBotMessagesQueryHandler : IRequestHandler<GetUserChatBotMessagesQuery, PaginatedList<UserChatBotMessageDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUser _currentUser;

        public GetUserChatBotMessagesQueryHandler(IApplicationDbContext context, IMapper mapper, ICurrentUser currentUser)
        {
            _context = context;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<PaginatedList<UserChatBotMessageDto>> Handle(GetUserChatBotMessagesQuery request, CancellationToken cancellationToken)
        {
            var query = _context.UserChatBotMessages
                .Where(msg => msg.OwnerId == _currentUser.Id)
                .OrderByDescending(msg => msg.MessagedOn)
                .AsQueryable();

            if (request.Before != null)
                query = query.Where(msg => msg.MessagedOn < request.Before);

            return await query.ProjectTo<UserChatBotMessageDto>(_mapper.ConfigurationProvider)
                .PaginateAsync(request.PageNumber, request.PageSize);
        }
    }
}
