using Microsoft.AspNetCore.Http;

namespace Application.Authors.Commands.CreateAuthor
{
    public class CreateAuthorCommand: IRequest<int>
    {
        public string Name { get; set; }
    }
}
