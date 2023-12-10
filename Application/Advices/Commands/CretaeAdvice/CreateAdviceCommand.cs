using Microsoft.AspNetCore.Http;

namespace Application.MedicalAdvices.Commands.CretaeMedicalAdvice
{
    public class CreateAdviceCommand : IRequest<int>
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public IFormFile Image { get; set; }
        public DateTime PublishedON { get; set; }
        public int AuthorId { get; set; }
    }
}
