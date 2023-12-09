using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Articles.Commands.CreateArticle
{
    public class CreateArticleCommandValidator : AbstractValidator<CreateArticleCommand>
    {
        public CreateArticleCommandValidator(IApplicationDbContext context) 
        {
            RuleFor(c => c.Title)
                .NotEmpty()
                .MaximumLength(200)
                .MustAsync(async (n, ct) => !await context.Articles.AnyAsync(a => a.Title == n))
                .WithMessage("Article already exists");
            
            RuleFor(c => c.PublicationDate).NotEmpty();
            RuleFor(c => c.Content).NotEmpty();
            RuleFor(c => c.AuthorId).NotEmpty();
            RuleFor(c => c.Image).Cascade(CascadeMode.Stop).NotEmpty().Image();
        }
    }
}
