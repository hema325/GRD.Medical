using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MedicalAdvices.Commands.CretaeMedicalAdvice
{
    public class CreateMedicalAdviceCommandValidator : AbstractValidator<CreateMedicalAdviceCommand>
    {
        public CreateMedicalAdviceCommandValidator(IApplicationDbContext Context)
        {

            RuleFor(ma => ma.Title)
                .NotEmpty()
                .MaximumLength(100)
                .MustAsync(async (T, ct) => !await Context.MedicalAdvices.AnyAsync(ma => ma.Title == T))
                .WithMessage("Title Already Exists");

            RuleFor(ma => ma.Content)
                 .NotEmpty()
                .MaximumLength(int.MaxValue);

            RuleFor(ma => ma.AuthorId)
                .NotEmpty()
                .MustAsync(async (Id, ct) => await Context.Authors.AnyAsync(ma => ma.Id == Id))
                .WithMessage("This Author Dont Exists");
            RuleFor(ma => ma.Image)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Image();




        }
    }
}
