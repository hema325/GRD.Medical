using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Account.Commands.UpdateDoctor
{
    public class UpdateDoctorCommandValidator: AbstractValidator<UpdateDoctorCommand>
    {
        public UpdateDoctorCommandValidator(IApplicationDbContext context)
        {
            RuleFor(c => c.FirstName)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(c => c.LastName)
                .NotEmpty()
                .MaximumLength(20);


            RuleFor(c => c.ConsultFee)
                .NotEmpty()
                .PrecisionScale(9, 2, true);

            RuleFor(c => c.SpecialityId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MustAsync(async (i, ct) => await context.Specialities.AnyAsync(s => s.Id == i, ct))
                .WithMessage("Specialist Id wasn't found.");

            RuleFor(c => c.LanguageIds)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .ForEach(builder =>
                {
                    builder
                    .Cascade(CascadeMode.Stop)
                    .MustAsync(async (i, ct) => await context.Languages.AnyAsync(l => l.Id == i))
                    .WithMessage("One or more language ids wasn't found.");
                });

            RuleFor(c => c.Experience)
                .NotEmpty()
                .LessThanOrEqualTo(60);
        }
    }
}
