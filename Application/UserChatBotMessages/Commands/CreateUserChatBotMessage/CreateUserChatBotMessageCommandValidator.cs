using FluentValidation;

namespace Application.UserChatBotMessages.Commands.CreateUserChatBotMessage
{
    public class CreateUserChatBotMessageCommandValidator: AbstractValidator<CreateUserChatBotMessageCommand>
    {
        public CreateUserChatBotMessageCommandValidator()
        {
            RuleFor(c => c.Message)
                .NotEmpty()
                .MaximumLength(500);
        }
    }
}
