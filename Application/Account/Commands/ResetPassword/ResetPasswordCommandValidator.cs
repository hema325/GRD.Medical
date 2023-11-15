using FluentValidation;

namespace Application.Account.Commands.ResetPassword
{
    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidator()
        {
            RuleFor(c => c.Password)
                 .NotEmpty()
                 .Password();
        }
    }
}
