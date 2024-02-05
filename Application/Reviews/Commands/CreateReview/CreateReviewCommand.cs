namespace Application.Reviews.Commands.CreateReview
{
    public class CreateReviewCommand: IRequest<int>
    {
        public double Stars { get; set; }
        public string Content { get; set; }
        public int DoctorId { get; set; }
    }
}
