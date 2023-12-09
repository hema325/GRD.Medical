using Microsoft.AspNetCore.Http;

namespace Application.Authors.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand: IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
