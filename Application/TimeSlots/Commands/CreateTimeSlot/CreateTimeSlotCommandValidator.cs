using FluentValidation;

namespace Application.TimeSlots.Commands.CreateTimeSlot
{
    public class CreateTimeSlotCommandValidator: AbstractValidator<CreateTimeSlotCommand>
    {
        public CreateTimeSlotCommandValidator()
        {
            RuleFor(c => c.Start)
                .NotEmpty()
                .Must(s=> IsTimeSpan(s))
                .WithMessage("Start is not a valid time.");

            RuleFor(c => c.End)
                .NotEmpty().Must(e => IsTimeSpan(e))
                .WithMessage("End is not a valid time.");

            RuleFor(c => c.Day)
                .NotEmpty()
                .IsEnumName(typeof(Days))
                .WithMessage("Day value in not valid");
        }

        private bool IsTimeSpan(string timeStr) =>
        TimeSpan.TryParse(timeStr, out var time);
    }
}
