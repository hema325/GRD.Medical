using Application.Notifications.Commands.CreateNotification;
using Domain.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Application.Common.Extensions;
using Microsoft.Extensions.Caching.Distributed;

namespace Infrastructure.BackgroundJobs.Reminder.Services
{
    internal class ReminderService: IReminderService
    {
        private readonly IApplicationDbContext _context;
        private readonly INotificationSender _notificationSender;
        private readonly ISender _mediatorSender;
        private readonly IDistributedCache _cache;
        private readonly ReminderSettings _settings;

        public ReminderService(IApplicationDbContext context,
                               INotificationSender notificationSender,
                               ISender mediatorSender,
                               IDistributedCache cache,
                               IOptions<ReminderSettings> settings)
        {
            _context = context;
            _notificationSender = notificationSender;
            _mediatorSender = mediatorSender;
            _cache = cache;
            _settings = settings.Value;
        }

        public async Task RemindAsync()
        {
            var appointments = await GetAppointments();

            foreach (var appointment in appointments)
            {
                var diffSpan = appointment.StartDateTime - DateTime.UtcNow;

                if (diffSpan < TimeSpan.FromMinutes(11) && appointment.RemindedTimes == 0)
                {
                    await NotifyUsers(appointment);
                    await UpdateRemindedTimes(appointment);
                    await Cache(appointments);
                }

                if (diffSpan <= TimeSpan.Zero && appointment.RemindedTimes == 1)
                {
                    await NotifyUsers(appointment);
                    await UpdateRemindedTimes(appointment);
                    await Cache(appointments);
                }
            }
        }

        private async Task NotifyUsers(Appointment appointment)
        {
            var diffSpan = appointment.StartDateTime - DateTime.UtcNow;

            //declare notifications
            var contentFormat = diffSpan <= TimeSpan.Zero ? NotificationTemplates.AppointmentAvailable : NotificationTemplates.AppointmentReminder;
            var doctorNotification = new CreateNotificationCommand
            {
                ReferenceType = ReferenceTypes.Reminder,
                ReferenceId = appointment.Id,
                OwnerId = appointment.DoctorId,
                Content = string.Format(contentFormat, diffSpan.Minutes)
            };

            var patientNotification = new CreateNotificationCommand
            {
                ReferenceType = ReferenceTypes.Reminder,
                ReferenceId = appointment.Id,
                OwnerId = appointment.PatientId,
                Content = string.Format(contentFormat, diffSpan.Minutes)
            };

            //create notification 
            var doctorNotificationDto = await _mediatorSender.Send(doctorNotification);
            var patientNotificationDto = await _mediatorSender.Send(patientNotification);

            //send notifications to clients
            await _notificationSender.SendToUserAsync(doctorNotification.OwnerId, doctorNotificationDto);
            await _notificationSender.SendToUserAsync(patientNotification.OwnerId, patientNotificationDto);
        }

        private async Task UpdateRemindedTimes(Appointment appointment)
        {
            appointment.RemindedTimes += 1;
            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();
        }

        private async Task<IEnumerable<Appointment>> GetAppointments()
        {
            var appointments = await _cache.GetAsync<List<Appointment>>(CacheKeys.ReminderAppointments);

            if (appointments == null)
            {
                appointments = await _context.Appointments
                    .Where(a => a.StartDateTime.AddMinutes(-15) <= DateTime.UtcNow && a.StartDateTime >= DateTime.UtcNow)
                    .ToListAsync();

                await Cache(appointments);
            }

            return appointments;
        }

        private async Task Cache(IEnumerable<Appointment>? appointments)
        {
            await _cache.SetAsync(CacheKeys.ReminderAppointments, appointments, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_settings.CacheInMinutes)
            });
        }
    }
}
