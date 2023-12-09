using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Article : EntityBase
    {
        public string Title { get; set; }
        public DateTime PublicationDate {  get; set; } 
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
