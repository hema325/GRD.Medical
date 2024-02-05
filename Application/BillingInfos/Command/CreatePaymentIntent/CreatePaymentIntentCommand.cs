namespace Application.BillingInfos.Command.CreatePaymentIntent
{
    public class CreatePaymentIntentCommand: IRequest<PaymentIntentDto>
    {
        public int DoctorId { get; set; }
    }
}
