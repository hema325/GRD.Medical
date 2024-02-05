using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Appointments.Commands.CreateAppointment
{
    public class CreateAppointmentCommandValidator: AbstractValidator<CreateAppointmentCommand>
    {
        public CreateAppointmentCommandValidator(IApplicationDbContext context)
        {
            RuleFor(c => c.Date)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Must(d => IsDate(d))
                    .WithMessage("Date is not a valid date.")
                .Must(d => DateTime.Parse(d).Date >= DateTime.UtcNow.Date)
                    .WithMessage("Can not make an appointment in the past.");

            RuleFor(c => c.DoctorId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MustAsync(async (id,ct) => await context.Users.AnyAsync(u=>u.Id == id))
                    .WithMessage("Doctor id wasn't found.");

            RuleFor(c => c.TimeSlotId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MustAsync(async (id, ct) => await context.TimeSlots.AnyAsync(u => u.Id == id))
                    .WithMessage("Time slot id wasn't found.")
                .MustAsync(async (c, id, ct) => await context.TimeSlots.AnyAsync(ts => ts.Id == id && ts.Doctor.User.Id == c.DoctorId))
                    .WithMessage("Time slot is not owned for this doctor.")
                .MustAsync(async (c, id, ct) => !await context.Appointments.AnyAsync(a => context.TimeSlots.Any(ts=> ts.Id == c.TimeSlotId &&
                        ts.Start == a.StartDateTime.TimeOfDay && 
                        ts.End == a.EndDateTime.TimeOfDay) &&
                        a.StartDateTime.Date == DateTime.Parse(c.Date).Date &&
                        a.Status != AppointmentStatuses.Completed))
                    .When(c => IsDate(c.Date), ApplyConditionTo.CurrentValidator)
                    .WithMessage("Time slot is not available.");


            RuleFor(c => c.PaymentIntentId)
                .NotEmpty();
        }

        private bool IsDate(string dateStr)
            => DateOnly.TryParse(dateStr, out var date);
    }
}
