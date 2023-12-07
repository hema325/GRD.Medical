using FluentValidation;

namespace Application.Account.Commands.UpdateUser
{
    public class UpdateUserCommandValidator: AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(c => c.FirstName)
                .NotEmpty()
                .MaximumLength(20); 
            
            RuleFor(c => c.LastName)
                .NotEmpty()
                .MaximumLength(20);
        }
    }
}
