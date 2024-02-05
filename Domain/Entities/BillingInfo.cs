namespace Domain.Entities
{
    public class BillingInfo: EntityBase
    {
        public decimal Amount { get; set; }
        public string PaymentIntentId { get; set; }
        public int AppointmentId { get; set; }
        public DateTime PaidIn { get; set; }
        public Appointment Appointment { get; set; }
    }
}
