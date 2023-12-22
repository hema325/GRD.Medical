using Application.Common.Models;
using Domain.OwnedEntities;

namespace Application.Common.Mappings.Reslovers
{
    internal class MediaUrlResolver: IValueResolver<Media ,MediaDto, string>
    {
        private readonly ICurrentHttpRequest _httpRequest;

        public MediaUrlResolver(ICurrentHttpRequest httpRequest)
        {
            _httpRequest = httpRequest;
        }

        public string Resolve(Media source, MediaDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Url))
            {
                var scheme = _httpRequest.Scheme;
                var host = _httpRequest.Host;
                return $"{scheme}://{host}/{source.Url}";
            }

            return null;
        }
    }
}
