using FluentValidation;

namespace Application.Notifications.Commands.CreateNotification
{
    public class CreateNotificationCommandValidator : AbstractValidator<CreateNotificationCommand>
    {
        public CreateNotificationCommandValidator()
        {
            RuleFor(n => n.Content)
                .NotEmpty()
                .MaximumLength(450);
        }
    }
}
