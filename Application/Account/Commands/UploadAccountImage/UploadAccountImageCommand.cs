using Microsoft.AspNetCore.Http;

namespace Application.Account.Commands.UploadAccountImage
{
    public class UploadAccountImageCommand: IRequest
    {
        public IFormFile Image { get; set; }
    }
}
