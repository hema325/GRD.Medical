using FluentValidation;

namespace Application.Account.Commands.ChangePassword
{
    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator()
        {
            RuleFor(c => c.NewPassword)
                .NotEmpty()
                .Password();
        }
    }
}
