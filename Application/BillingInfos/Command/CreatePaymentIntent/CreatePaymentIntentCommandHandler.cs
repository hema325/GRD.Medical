using Application.Common.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Application.BillingInfos.Command.CreatePaymentIntent
{
    internal class CreatePaymentIntentCommandHandler : IRequestHandler<CreatePaymentIntentCommand, PaymentIntentDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IPayment _payment;
        private readonly IDistributedCache _distributedCache;
        private readonly ICurrentUser _currentUser;

        public CreatePaymentIntentCommandHandler(IPayment payment, IDistributedCache distributedCache, ICurrentUser currentUser, IApplicationDbContext context)
        {
            _payment = payment;
            _distributedCache = distributedCache;
            _currentUser = currentUser;
            _context = context;
        }

        public async Task<PaymentIntentDto> Handle(CreatePaymentIntentCommand request, CancellationToken cancellationToken)
        {
            var intentKey = string.Format(CacheKeys.PaymentIntentId, _currentUser.Id, request.DoctorId);
            var paymentIntent = await _distributedCache.GetAsync<PaymentIntentDto>(intentKey);

            if (paymentIntent == null)
            {
                var doctor = await _context.Doctors
                    .FirstOrDefaultAsync(u => u.UserId == request.DoctorId);

                paymentIntent = await _payment.CreatePaymentIntentAsync(doctor.ConsultFee);
                await _distributedCache.SetAsync(intentKey, paymentIntent);
            }

            return paymentIntent;
        }
    }
}
