using Application.Common.Models;
using Microsoft.AspNetCore.Http;

namespace Application.Account.Commands.UploadAccountImage
{
    public class UploadAccountImageCommand: IRequest<UploadAccountImageCommandResponse>
    {
        public IFormFile Image { get; set; }
    }
}
