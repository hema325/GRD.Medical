using Application.Common.Models.EmailTemplates;
using Application.Notifications.Commands.CreateNotification;
using Microsoft.EntityFrameworkCore;

namespace Application.Appointments.Events
{
    internal class AppointmentCreatedEventHandler : INotificationHandler<AppointmentCreatedEvent>
    {
        private readonly IApplicationDbContext _context;
        private readonly IEmailSender _emailSender;
        private readonly IClientSettings _client;
        private readonly INotificationSender _notificationSender;
        private readonly ISender _sender;
        private readonly ICurrentUser _currentUser;

        public AppointmentCreatedEventHandler(IEmailSender emailSender, IApplicationDbContext context, IClientSettings client, INotificationSender notificationSender, ISender sender, ICurrentUser currentUser)
        {
            _emailSender = emailSender;
            _context = context;
            _client = client;
            _notificationSender = notificationSender;
            _sender = sender;
            _currentUser = currentUser;
        }

        public async Task Handle(AppointmentCreatedEvent notification, CancellationToken cancellationToken)
        {
            var appointment = notification.Appointment;
            var t1 = NotifyOnEmail(appointment);
            var t2 = NotifyClients(appointment);

            await Task.WhenAll(t1, t2);
        }

        private async Task NotifyClients(Appointment appointment)
        {
            var notificationCommand = new CreateNotificationCommand
            {
                ReferenceId = appointment.Id,
                Content = string.Format(NotificationTemplates.AppointmentSchedule, _currentUser.Name, AppointmentDateToString(appointment)),
                ReferenceType = ReferenceTypes.Appointment,
                OwnerId = appointment.DoctorId,
                InitiatorId = _currentUser.Id
            };

            var notificationDto = await _sender.Send(notificationCommand);
            await _notificationSender.SendToUserAsync(notificationCommand.OwnerId, notificationDto);
        }

        private async Task NotifyOnEmail(Appointment appointment)
        {
            var templateObj = new EmailAppointmentScheduledTemplate
            {
                DateTime = AppointmentDateToString(appointment),
                AppointmentUrl = _client.BaseUrl + $"/appointments/chat-count-down?appontId={appointment.Id}"
            };

            var emails = await _context.Users
                .Where(u => u.Id == appointment.PatientId || u.Id == appointment.DoctorId)
                .Select(u => u.Email)
                .ToListAsync();

            await Task.WhenAll(emails.Select(email => _emailSender.SendEmailAppointmentSheduledAsync(email, templateObj)));
        }

        private string AppointmentDateToString(Appointment appointment)
        {
            return appointment.StartDateTime.ToShortDateString() + " " +
                appointment.StartDateTime.ToString("hh:mm tt");
        }
    }
}
