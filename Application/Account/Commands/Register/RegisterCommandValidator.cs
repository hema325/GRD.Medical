using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Account.Commands.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator(IApplicationDbContext context)
        {
            RuleFor(c => c.FirstName)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(c => c.LastName)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(c => c.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(250)
                .MustAsync(async (e, ct) => !await context.Users.AnyAsync(u => u.Email == e, ct));

            RuleFor(c => c.Password)
                .NotEmpty()
                .Password();
        }
    }
}
