using Application.BillingInfos.Command;
using Microsoft.Extensions.Options;
using Stripe;

namespace Infrastructure.Payment
{
    internal class StripeService: IPayment
    {
        private readonly StripeSettings _settings;

        public StripeService(IOptions<StripeSettings> stripeSettings)
        {
            _settings = stripeSettings.Value;
            StripeConfiguration.ApiKey = _settings.Secretkey;
        }

        public async Task<PaymentIntentDto> CreatePaymentIntentAsync(decimal amount, string currency = "usd") { 

            var options = new PaymentIntentCreateOptions { 
                Amount = (long)(amount * 100),
                Currency = currency ,
                PaymentMethodTypes = new List<string>
                {
                    "card"
                }
            };

            var service = new PaymentIntentService();
            var intent = await service.CreateAsync(options);

            return new PaymentIntentDto
            {
                Id = intent.Id,
                ClientSecret = intent.ClientSecret
            };
        }

        public async Task UpdatePaymentIntentAsync(string intentId, long amount)
        {
            var options = new PaymentIntentUpdateOptions
            {
                Amount = amount
            };

            var service = new PaymentIntentService();
            await service.UpdateAsync(intentId, options);
        }

        public async Task<bool> IsPaymentSucceeded(string intentId)
        {
            var service = new PaymentIntentService();
            var paymentIntent = await service.GetAsync(intentId);

            if (paymentIntent.Status == "succeeded")
                return true;

            return false;
        }
    }
}
