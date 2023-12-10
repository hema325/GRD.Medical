using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Articles.Commands.UpdateArticle
{
    public class UpdateArticleCommandValidator : AbstractValidator<UpdateArticleCommand>
    {
        public UpdateArticleCommandValidator(IApplicationDbContext context)
        {
            RuleFor(c => c.Title)
                .NotEmpty()
                .MaximumLength(200)
                .MustAsync(async (c, n, ct) => !await context.Articles.AnyAsync(a => a.Title == n && c.Id != a.Id))
                .WithMessage("Article already exists");

            RuleFor(c => c.PublishedOn)
                .NotEmpty();

            RuleFor(c => c.Content)
                .NotEmpty();

            RuleFor(c => c.AuthorId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MustAsync(async (id, ct) => await context.Authors.AnyAsync(a => a.Id == id))
                .WithMessage("Author id wasn't found.");

            RuleFor(c => c.Image)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Image();
        }
    }
}
