using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Articles.Commands.DeleteAritcle
{
    public class DeleteArticleCommand : IRequest
    {
        public int Id { get; set; }
    }
}
