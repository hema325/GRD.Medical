using Application.Articles.Commands.CreateArticle;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MedicalInformation.Commands.UpdateMedicalInfo
{
    public class UpdateMedicalInfoCommandValidator : AbstractValidator<UpdateMedicalInfoCommand>
    {
        public UpdateMedicalInfoCommandValidator(IApplicationDbContext context)
        {
            RuleFor(c => c.Age)
                 .GreaterThan(0)
                 .WithMessage("The value must be greater than 0.");

            RuleFor(c => c.Hight)
                 .GreaterThan(0)
                 .WithMessage("The value must be greater than 0.");

            RuleFor(c => c.Wight)
                 .GreaterThan(0)
                 .WithMessage("The value must be greater than 0.");
        }
    }
}
