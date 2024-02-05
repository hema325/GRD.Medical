using FluentValidation;

namespace Application.TimeSlots.Commands.CreateTimeSlot
{
    public class CreateTimeSlotCommandValidator: AbstractValidator<CreateTimeSlotCommand>
    {
        public CreateTimeSlotCommandValidator()
        {
            RuleFor(c => c.Start)
                .NotEmpty()
                .Must(s=> IsTime(s))
                    .WithMessage("Start is not a valid time.");

            RuleFor(c => c.End)
                .NotEmpty()
                .Must(e => IsTime(e))
                    .WithMessage("End is not a valid time.");

            RuleFor(c => c.Day)
                .NotEmpty()
                .IsEnumName(typeof(Days))
                    .WithMessage("Day value in not valid");

            RuleFor(c=>c)
                .Must(c=>TimeOnly.Parse(c.End) > TimeOnly.Parse(c.Start))
                    .When(c => IsTime(c.Start) && IsTime(c.End))
                    .WithMessage("Only intervals within the same date are allowed.");
        }

        private bool IsTime(string timeStr) =>
        TimeOnly.TryParse(timeStr, out var time);
    }
}
