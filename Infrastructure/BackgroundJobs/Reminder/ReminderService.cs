using Application.Notifications.Commands.CreateNotification;
using Domain.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Infrastructure.BackgroundJobs.Reminder
{
    internal class ReminderService : BackgroundService
    {
        private readonly IDistributedCache _cache;
        private readonly ReminderSettings _settings;
        private readonly IServiceProvider _serviceProvider;

        public ReminderService(IOptions<ReminderSettings> settings, IDistributedCache cache, IServiceProvider serviceProvider)
        {
            _settings = settings.Value;
            _cache = cache;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var appointments = await GetAppointments();

                foreach (var appointment in appointments)
                {
                    var diffSpan = appointment.StartDateTime - DateTime.UtcNow;

                    if (diffSpan <= TimeSpan.FromMinutes(10) && appointment.RemindedTimes == 0)
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

                await Task.Delay(_settings.DelayInSeconds * 1000);
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
            var mediatorSender = GetMediatorSender();
            var doctorNotificationDto = await mediatorSender.Send(doctorNotification);
            var patientNotificationDto = await mediatorSender.Send(patientNotification);

            //send notifications to clients
            var notificationSender = GetNotificationSender();
            await notificationSender.SendToUserAsync(doctorNotification.OwnerId, doctorNotificationDto);
            await notificationSender.SendToUserAsync(patientNotification.OwnerId, patientNotificationDto);
        }

        private async Task UpdateRemindedTimes(Appointment appointment)
        {
            var context = GetApplicationContext();
            appointment.RemindedTimes += 1;
            context.Appointments.Update(appointment);
            await context.SaveChangesAsync();
        }

        private async Task<IEnumerable<Appointment>> GetAppointments()
        {
            var appointmentsJson = await _cache.GetStringAsync(CacheKeys.ReminderAppointments);
            IEnumerable<Appointment>? appointments = null;

            if (appointmentsJson != null)
                appointments = JsonSerializer.Deserialize<IEnumerable<Appointment>>(appointmentsJson);

            if (appointments == null)
            {
                var context = GetApplicationContext();
                appointments = await context.Appointments
                    .Where(a => a.StartDateTime.AddMinutes(-15) <= DateTime.UtcNow && a.StartDateTime >= DateTime.UtcNow)
                    .ToListAsync();

                await Cache(appointments);
            }

            return appointments;
        }

        private async Task Cache(IEnumerable<Appointment>? appointments)
        {
            string? appointmentsJson = JsonSerializer.Serialize(appointments);
            await _cache.SetStringAsync(CacheKeys.ReminderAppointments, appointmentsJson, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_settings.CacheInMinutes)
            });
        }

        private IApplicationDbContext GetApplicationContext()
        {
            var scope = _serviceProvider.CreateScope();
            return scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
        }

        private ISender GetMediatorSender()
        {
            var scope = _serviceProvider.CreateScope();
            return scope.ServiceProvider.GetRequiredService<ISender>();
        }

        private INotificationSender GetNotificationSender()
        {
            var scope = _serviceProvider.CreateScope();
            return scope.ServiceProvider.GetRequiredService<INotificationSender>();
        }

    }
}
