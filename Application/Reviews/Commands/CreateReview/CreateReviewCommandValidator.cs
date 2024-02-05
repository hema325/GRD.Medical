using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Reviews.Commands.CreateReview
{
    public class CreateReviewCommandValidator: AbstractValidator<CreateReviewCommand>
    {
        public CreateReviewCommandValidator(IApplicationDbContext context)
        {
            RuleFor(c => c.Content)
                .NotEmpty()
                .MaximumLength(500);

            RuleFor(c => c.Stars)
                .NotEmpty()
                .LessThanOrEqualTo(5)
                .GreaterThanOrEqualTo(.5);

            RuleFor(c => c.DoctorId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MustAsync(async (i, ct) => await context.Users.AnyAsync(u => u.Id == i))
                .WithMessage("Doctor Id wasn't found.");
        }
    }
}
