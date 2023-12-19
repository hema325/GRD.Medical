using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Articles.Commands.CreateArticle
{
    public class CreateArticleCommandValidator : AbstractValidator<CreateArticleCommand>
    {
        public CreateArticleCommandValidator(IApplicationDbContext context) 
        {
            RuleFor(c => c.Title)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MaximumLength(200)
                .MustAsync(async (n, ct) => !await context.Articles.AnyAsync(a => a.Title == n))
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
                .NotEmpty()
                .Image();
        }
    }
}
