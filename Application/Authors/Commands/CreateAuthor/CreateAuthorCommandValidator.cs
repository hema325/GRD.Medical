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

            RuleFor(c => c.JobTitle)
                .NotEmpty()
                .MaximumLength(80);

            RuleFor(c => c.Image)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Image();
        }
    }
}
