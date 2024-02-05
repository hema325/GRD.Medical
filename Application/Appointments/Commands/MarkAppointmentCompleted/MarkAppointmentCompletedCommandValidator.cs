using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Appointments.Commands.MarkAppointmentCompleted
{
    public class MarkAppointmentCompletedCommandValidator: AbstractValidator<MarkAppointmentCompletedCommand>
    {
        public MarkAppointmentCompletedCommandValidator(IApplicationDbContext context)
        {
            RuleFor(c => c.Id)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MustAsync(async (i, ct) => await context.Appointments.AnyAsync(a => a.Id == i))
                .WithMessage("Appointment wasn't found.");
        }
    }
}
