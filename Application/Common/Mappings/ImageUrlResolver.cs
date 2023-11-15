using Microsoft.AspNetCore.Http;

namespace Application.Common.Mappings
{
    internal class ImageUrlResolver : IValueResolver<object, object, string?>
    {
        private readonly HttpContext _httpContext;

        public ImageUrlResolver(IHttpContextAccessor accessor)
        {
            _httpContext = accessor.HttpContext;
        }

        public string Resolve(dynamic source, object destination, string? destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.ImageUrl))
            {
                var scheme = _httpContext.Request.Scheme;
                var host = _httpContext.Request.Host;
                return $"{scheme}://{host}/{source.ImageUrl}";
            }

            return null;
        }
    }
}
