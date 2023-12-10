using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MedicalAdvices.Commands.CretaeMedicalAdvice
{
    public class CreateMedicalAdviceCommand : IRequest<int>
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public IFormFile Image { get; set; }

        //public DateTime PublishedON { get; set; }
        public int AuthorId { get; set; }
    }
}
