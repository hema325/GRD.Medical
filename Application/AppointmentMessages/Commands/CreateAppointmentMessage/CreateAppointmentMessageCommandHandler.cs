using Application.AppointmentMessages.Commands.Queries;
using Domain.Entities.OwnedEntities;

namespace Application.AppointmentMessages.Commands.CreateAppointmentMessage
{
    internal class CreateAppointmentMessageCommandHandler : IRequestHandler<CreateAppointmentMessageCommand, AppointmentMessageDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUser _currentUser;
        private readonly IFileStorage _fileStorage;
        private readonly IAppointmentChat _appointmentChat;

        public CreateAppointmentMessageCommandHandler(IApplicationDbContext context,
                                                      IMapper mapper,
                                                      ICurrentUser currentUser,
                                                      IFileStorage fileStorage,
                                                      IAppointmentChat appointmentChat)
        {
            _context = context;
            _mapper = mapper;
            _currentUser = currentUser;
            _fileStorage = fileStorage;
            _appointmentChat = appointmentChat;
        }

        public async Task<AppointmentMessageDto> Handle(CreateAppointmentMessageCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _context.Appointments.FindAsync(request.AppointmentId);

            ValidateAppointment(appointment);

            //create message
            var appointmentMessage = new AppointmentMessage
            {
                Content = request.Content,
                MessagedOn = DateTime.UtcNow,
                AppointmentId = request.AppointmentId,
                SenderId = _currentUser.Id.Value
            };

            if (request.Images != null && request.Images.Any())
                appointmentMessage.Medias = (await Task.WhenAll(request.Images.Select(img => _fileStorage.SaveAsync(img))))
                    .Select(imgUrl => new Media { Type = MediaTypes.Image, Url = imgUrl }).ToList();

            appointmentMessage.AddDomainEvent(new EntityCreatedEvent(appointmentMessage));
            _context.AppointmentMessages.Add(appointmentMessage);
            await _context.SaveChangesAsync();

            appointmentMessage.Sender = await _context.Users.FindAsync(_currentUser.Id);
            var msgDto = _mapper.Map<AppointmentMessageDto>(appointmentMessage);

            await SendToUser(appointment, msgDto);

            return msgDto;
        }

        private void ValidateAppointment(Appointment? appointment)
        {
            if (appointment.PatientId != _currentUser.Id &&
                            appointment.DoctorId != _currentUser.Id)
                throw new ForbiddenException("You are not part of this appointment.");

            if (DateTime.UtcNow < appointment.StartDateTime &&
                DateTime.UtcNow > appointment.EndDateTime)
                throw new ForbiddenException("this appointment is completed or didn't start yet.");
        }

        private async Task SendToUser(Appointment? appointment, AppointmentMessageDto msgDto)
        {
            if (appointment.PatientId == _currentUser.Id)
                await _appointmentChat.SendToUserAsync(appointment.DoctorId, msgDto);
            else
                await _appointmentChat.SendToUserAsync(appointment.PatientId, msgDto);
        }
    }
}
