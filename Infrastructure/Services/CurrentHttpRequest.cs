using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services
{
    internal class CurrentHttpRequest: ICurrentHttpRequest
    {
        private readonly HttpRequest? _httpRequest;
        public CurrentHttpRequest(IHttpContextAccessor accessor)
        {
            _httpRequest = accessor.HttpContext?.Request;   
        }

        public string? Scheme => _httpRequest?.Scheme;
        public string? Host => _httpRequest?.Host.Value;
    }
}
