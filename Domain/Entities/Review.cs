namespace Domain.Entities
{
    public class Review: EntityBase
    {
        public double Stars { get; set; }
        public string Content { get; set; }
        public DateTime ReviewedOn { get; set; }
        public int DoctorId { get; set; }
        public int OwnerId { get; set; }

        public User Owner { get; set; }
        public Doctor Doctor { get; set; }
    }
}
