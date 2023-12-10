namespace Application.MedicalAdvices.Queries.GetMedicalAdviceByID
{
    public class GetAdviceByIdQuary : IRequest<MedicalAdviceDto>
    {
        public int Id { get; set; }
    }
}
