using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Authors.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidator: AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator(IApplicationDbContext context)
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .MaximumLength(80)
                .MustAsync(async (c, n, ct) => !await context.Authors.AnyAsync(a => a.Name == n && a.Id != c.Id, ct))
                .WithMessage("Name already exists");

        }
    }
}
