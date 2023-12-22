using FluentValidation;

namespace Application.Posts.Commands.CreatePost
{
    public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
    {
        public CreatePostCommandValidator()
        {
            RuleFor(c => c.Content)
                .MaximumLength(5000);

            RuleFor(c => c.Images)
                .ForEach(f => f.Image());

            RuleFor(c => c)
                .Must(c => c.Images != null && c.Images.Count() > 0 || !string.IsNullOrEmpty(c.Content))
                .WithMessage("Content or images fields must not be empty.");
        }
    }
}
