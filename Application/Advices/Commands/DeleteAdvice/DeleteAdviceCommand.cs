namespace Application.MedicalAdvices.Commands.DeleteMedicalAdvice
{
    public class DeleteAdviceCommand:IRequest
    {
        public int Id { get; set; }
    }
}
