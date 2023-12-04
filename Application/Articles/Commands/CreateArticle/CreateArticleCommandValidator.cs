using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Articles.Commands.CreateArticle
{
    public class CreateArticleCommandValidator : AbstractValidator<CreateArticleCommand>
    {
        public CreateArticleCommandValidator() 
        { 
            RuleFor(c => c.Title).NotEmpty().MaximumLength(200);
            RuleFor(c => c.PublicationDate).NotEmpty();
            RuleFor(c => c.Content).NotEmpty();
            RuleFor(c => c.AuthorId).NotNull();
            RuleFor(c => c.Image).Cascade(CascadeMode.Stop).NotEmpty().Image();
        }
    }
}
