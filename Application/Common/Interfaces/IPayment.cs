using Application.BillingInfos.Command;

namespace Application.Common.Interfaces
{
    public interface IPayment
    {
        Task<PaymentIntentDto> CreatePaymentIntentAsync(decimal amount, string currency = "usd");
        Task<bool> IsPaymentSucceeded(string intentId);
        Task UpdatePaymentIntentAsync(string intentId, long amount);
    }
}
