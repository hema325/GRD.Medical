using Application.UserChatBotMessages.Queries;

namespace Application.UserChatBotMessages.Commands.CreateUserChatBotMessage
{
    internal class CreateUserChatBotMessageCommandHandler : IRequestHandler<CreateUserChatBotMessageCommand, CreateUserChatBotMessageCommandResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUser _currentUser;
        private readonly IChatBot _chatBot;
        private readonly IMapper _mapper;

        public CreateUserChatBotMessageCommandHandler(IApplicationDbContext context,
            ICurrentUser currentUser,
            IChatBot chatBot,
            IMapper mapper)
        {
            _context = context;
            _currentUser = currentUser;
            _chatBot = chatBot;
            _mapper = mapper;
        }

        public async Task<CreateUserChatBotMessageCommandResponse> Handle(CreateUserChatBotMessageCommand request, CancellationToken cancellationToken)
        {
            var userMessage = new UserChatBotMessage
            {
                Content = request.Message,
                MessagedOn = DateTime.UtcNow,
                OwnerId = _currentUser.Id.Value,
                IsBotMessage = false
            };

            var chatBotResult = await _chatBot.GetResponseAsync(request.Message);
            UserChatBotMessage chatBotMessage = null;

            if (chatBotResult.Succeeded)
            {
                chatBotMessage = new UserChatBotMessage
                {
                    Content = chatBotResult.Message,
                    MessagedOn = DateTime.UtcNow,
                    OwnerId = _currentUser.Id.Value,
                    IsBotMessage = true
                };

                _context.UserChatBotMessages.Add(chatBotMessage);
            }
            
            _context.UserChatBotMessages.Add(userMessage);
            await _context.SaveChangesAsync();

            return new CreateUserChatBotMessageCommandResponse
            {
                UserMessage = _mapper.Map<UserChatBotMessageDto>(userMessage),
                ChatBotMessage = _mapper.Map<UserChatBotMessageDto>(chatBotMessage),
                IsFailedToGenerateResponse = !chatBotResult.Succeeded
            };
        }
    }
}
