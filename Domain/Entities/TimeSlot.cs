namespace Domain.Entities
{
    public class TimeSlot : EntityBase
    {
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
        public Days Day { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get;set;}
    }
}
