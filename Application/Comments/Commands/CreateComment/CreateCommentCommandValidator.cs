using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Comments.Commands.CreateComment
{
    public class CreateCommentCommandValidator: AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentCommandValidator(IApplicationDbContext context)
        {
            RuleFor(c => c.PostId)
                .NotEmpty()
                .MustAsync(async (id, ct) => await context.Posts.AnyAsync(p => p.Id == id))
                .WithMessage("PostId wasn't found.");

            RuleFor(c => c.Content)
                .MaximumLength(1500);

            RuleFor(c => c.Image)
                .Image();

            RuleFor(c => c.ReplyTo)
                .MustAsync(async (id, ct) => id == null || await context.Comments.AnyAsync(c => c.Id == id))
                .WithMessage("ReplyTo wasn't found.");

            RuleFor(c => c)
                .Must(c => c.Image != null || !string.IsNullOrEmpty(c.Content))
                .WithMessage("Content or images fields must not be empty.");
        }
    }
}
