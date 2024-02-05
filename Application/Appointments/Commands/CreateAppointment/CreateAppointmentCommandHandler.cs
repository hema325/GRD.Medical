using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace Application.Appointments.Commands.CreateAppointment
{
    internal class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IPayment _payment;
        private readonly IDistributedCache _cache;
        private readonly ICurrentUser _currentUser;
        public CreateAppointmentCommandHandler(IApplicationDbContext context,
                                               IPayment payment,
                                               IDistributedCache cache,
                                               ICurrentUser currentUser)
        {
            _context = context;
            _payment = payment;
            _cache = cache;
            _currentUser = currentUser;
        }

        public async Task<int> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            if (!await _payment.IsPaymentSucceeded(request.PaymentIntentId))
                throw new PaymentRequiredException();

            var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.UserId == request.DoctorId);
            var timeSlot = await _context.TimeSlots.FindAsync(request.TimeSlotId);

            var appointment = new Appointment
            {
                Status = AppointmentStatuses.Scheduled,
                StartDateTime = DateTime.Parse(request.Date) + timeSlot.Start,
                EndDateTime = DateTime.Parse(request.Date) + timeSlot.End,
                PatientId = _currentUser.Id.Value,
                DoctorId = request.DoctorId,
                BillingInfo = new BillingInfo
                {
                    Amount = doctor.ConsultFee,
                    PaymentIntentId = request.PaymentIntentId,
                    PaidIn = DateTime.UtcNow
                }
            };

            appointment.AddDomainEvent(new AppointmentCreatedEvent(appointment));
            appointment.AddDomainEvent(new EntityCreatedEvent(appointment));

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            var intentKey = string.Format(CacheKeys.PaymentIntentId, _currentUser.Id, request.DoctorId);
            await _cache.RemoveAsync(intentKey);

            return appointment.Id;
        }
    }
}
