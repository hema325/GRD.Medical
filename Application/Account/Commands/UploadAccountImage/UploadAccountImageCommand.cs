using Application.Common.Models;
using Microsoft.AspNetCore.Http;

namespace Application.Account.Commands.UploadAccountImage
{
    public class UploadAccountImageCommand: IRequest<UploadAccountImageCommandDto>
    {
        public IFormFile Image { get; set; }
    }
}
