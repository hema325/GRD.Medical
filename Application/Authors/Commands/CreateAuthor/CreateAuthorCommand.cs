using Microsoft.AspNetCore.Http;

namespace Application.Authors.Commands.CreateAuthor
{
    public class CreateAuthorCommand: IRequest<int>
    {
        public string Name { get; set; }
        public string JobTitle { get; set; }
        public IFormFile Image { get; set; }
    }
}
