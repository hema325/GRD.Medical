namespace Application.Common.Mappings
{
    internal class ImageUrlResolver : IValueResolver<object, object, string?>
    {
        private readonly ICurrentHttpRequest _httpRequest;

        public ImageUrlResolver(ICurrentHttpRequest httpRequest)
        {
            _httpRequest = httpRequest;
        }

        public string Resolve(dynamic source, object destination, string? destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.ImageUrl))
            {
                var scheme = _httpRequest.Scheme;
                var host = _httpRequest.Host;
                return $"{scheme}://{host}/{source.ImageUrl}";
            }

            return null;
        }
    }
}
