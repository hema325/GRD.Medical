using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Authors.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidator: AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator(IApplicationDbContext context)
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .MaximumLength(80)
                .MustAsync(async (n,ct) => !await context.Authors.AnyAsync(a=>a.Name == n))
                .WithMessage("Name already exists");
        }
    }
}
