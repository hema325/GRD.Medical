using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Application.Common.Validators
{
    internal static class ImageValidator
    {
        public static IRuleBuilderOptions<TSource, IFormFile> Image<TSource>(this IRuleBuilder<TSource, IFormFile> builder)
        {
            return builder.Must(f => f == null || f.ContentType.Contains("image"))
                .WithMessage("'{PropertyName}' is not an image");
        }
    }
}
