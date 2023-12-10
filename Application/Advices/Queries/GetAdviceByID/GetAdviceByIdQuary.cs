namespace Application.MedicalAdvices.Queries.GetMedicalAdviceByID
{
    public class GetAdviceByIdQuary : IRequest<AdviceDto>
    {
        public int Id { get; set; }
    }
}
