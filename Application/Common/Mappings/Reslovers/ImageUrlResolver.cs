namespace Application.Common.Mappings.Reslovers
{
    internal class ImageUrlResolver : IValueResolver<object, object, string?>
    {
        private readonly ICurrentHttpRequest _httpRequest;

        public ImageUrlResolver(ICurrentHttpRequest httpRequest)
        {
            _httpRequest = httpRequest;
        }

        public string? Resolve(dynamic source, object destination, string? destMember, ResolutionContext context)
        {
            return MediaHelpers.GetFullUrl(_httpRequest, source.ImageUrl);
        }
    }
}
