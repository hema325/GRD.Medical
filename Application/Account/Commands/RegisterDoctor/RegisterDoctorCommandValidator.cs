using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Account.Commands.RegisterDoctor
{
    public class RegisterDoctorCommandValidator: AbstractValidator<RegisterDoctorCommand>
    {
        public RegisterDoctorCommandValidator(IApplicationDbContext context)
        {
            RuleFor(c => c.FirstName)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(c => c.LastName)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(c => c.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(250)
                .MustAsync(async (e, ct) => !await context.Users.AnyAsync(u => u.Email == e, ct))
                .WithMessage("{PropertyName} has been taken");

            RuleFor(c => c.Password)
                .NotEmpty()
                .Password();

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
