using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.MedicalAdvices.Commands.CretaeMedicalAdvice
{
    public class CreateAdviceCommandValidator : AbstractValidator<CreateAdviceCommand>
    {
        public CreateAdviceCommandValidator(IApplicationDbContext Context)
        {
            RuleFor(a => a.Title)
                .NotEmpty()
                .MaximumLength(100)
                .MustAsync(async (t, ct) => !await Context.Advices.AnyAsync(a => a.Title == t))
                .WithMessage("Title already exists");

            RuleFor(a => a.Content)
                 .NotEmpty();

            RuleFor(a => a.AuthorId)
                .NotEmpty()
                .MustAsync(async (Id, ct) => await Context.Authors.AnyAsync(a => a.Id == Id))
                .WithMessage("Author id wasn't found.");

            RuleFor(a => a.Image)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Image();

            RuleFor(a => a.PublishedON)
                .NotEmpty();

        }
    }
}
