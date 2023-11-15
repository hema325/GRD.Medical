using FluentValidation;

namespace Application.Common.Validators
{
    internal static class PasswordValidator
    {
        public static IRuleBuilderOptions<TSource, string> Password<TSource>(this IRuleBuilder<TSource, string> builder)
        {
            return builder.Matches("(?=(.*[0-9]))(?=.*[\\!@#$%^&*()\\\\[\\]{}\\-_+=~`|:;\"'<>,./?])(?=.*[a-z])(?=(.*[A-Z]))(?=(.*)).{6,}")
                .WithMessage("'{PropertyName}' must be at leaset 6 letters containing upper/lower case, numbers and special cases");
        }
    }
}
