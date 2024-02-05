namespace Domain.Entities
{
    public class Appointment: EntityBase
    {
        public AppointmentStatuses Status { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public int RemindedTimes { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }

        public User Patient { get; set; }
        public User Doctor { get; set; }
        public BillingInfo BillingInfo { get; set; }
    }
}
