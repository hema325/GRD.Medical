using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.BillingInfos.Command.CreatePaymentIntent
{
    public class CreatePaymentIntentCommandValidator: AbstractValidator<CreatePaymentIntentCommand>
    {
        public CreatePaymentIntentCommandValidator(IApplicationDbContext context)
        {
            RuleFor(c => c.DoctorId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MustAsync(async (id, ct) => await context.Users.AnyAsync(u => u.Id == id && u.Role == Roles.Doctor, ct))
                .WithMessage("Doctor id wasn't found.");
        }
    }
}
