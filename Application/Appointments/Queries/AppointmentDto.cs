using Application.Users.Queries;

namespace Application.Appointments.Queries
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public UserDto Patient { get; set; }
        public UserDto Doctor { get; set; }
        public decimal ConsultFee { get; set; }

        private class Mapping: Profile
        {
            public Mapping()
            {
                CreateMap<Appointment, AppointmentDto>()
                    .ForMember(dto => dto.ConsultFee, opt => opt.MapFrom(a => a.BillingInfo.Amount));
            }
        }
    }
}
