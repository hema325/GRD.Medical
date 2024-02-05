using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.AppointmentMessages.Commands.CreateAppointmentMessage
{
    public class CreateAppointmentMessageCommandValidator: AbstractValidator<CreateAppointmentMessageCommand>
    {
        public CreateAppointmentMessageCommandValidator(IApplicationDbContext context)
        {
            RuleFor(c=>c.Content)
                .MaximumLength(500);

            RuleFor(c => c.Images)
                .ForEach(f => f.Image());

            RuleFor(c => c)
                .Must(c => !string.IsNullOrEmpty(c.Content) || (c.Images != null && c.Images.Any()))
                .WithMessage("Content or images fields must not be empty.");

            RuleFor(c => c.AppointmentId)
                .MustAsync(async (i, ct) => await context.Appointments.AnyAsync(a => a.Id == i, ct))
                .WithMessage("Appointment id wasn't found.");
        }
    }
}
