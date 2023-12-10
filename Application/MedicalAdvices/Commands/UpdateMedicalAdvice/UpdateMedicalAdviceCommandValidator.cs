using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MedicalAdvices.Commands.UpdateMedicalAdvice
{
    public class UpdateMedicalAdviceCommandValidator : AbstractValidator<UpdateMedicalAdviceCommand>
    {
        public UpdateMedicalAdviceCommandValidator(IApplicationDbContext Context)
        {
            RuleFor(ma => ma.Title)
                .NotEmpty()
                .MaximumLength(100)
                .MustAsync(async (i, T, ct) => !await Context.MedicalAdvices.AnyAsync(ma => ma.Title == T && ma.Id != i.Id))
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
