using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities.OwnedEntities;

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
            return MediaHelpers.GetFullUrl(_httpRequest,source.Url);
        }
    }
}
