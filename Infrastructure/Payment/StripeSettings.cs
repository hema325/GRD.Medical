namespace Infrastructure.Payment
{
    internal class StripeSettings
    {
        public const string SectionName = "Stripe";

        public string Secretkey { get; set; }
    }
}
