using Microsoft.AspNetCore.Http;

namespace Application.MedicalAdvices.Commands.UpdateMedicalAdvice
{
    public class UpdateAdviceCommand:IRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public IFormFile Image { get; set; }
        public DateTime PublishedON { get; set; }
        public int AuthorId { get; set; }

    }
}
