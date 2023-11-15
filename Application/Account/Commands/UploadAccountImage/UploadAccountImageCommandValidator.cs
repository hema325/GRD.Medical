using FluentValidation;

namespace Application.Account.Commands.UploadAccountImage
{
    public class UploadAccountImageCommandValidator: AbstractValidator<UploadAccountImageCommand>
    {
        public UploadAccountImageCommandValidator()
        {
            RuleFor(c => c.Image)
                .NotEmpty()
                .Image();
        }
    }
}
