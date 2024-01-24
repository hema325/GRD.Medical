using Application.Languages.Queries;
using Application.Specialists.Queries;

namespace Application.Users.Queries
{
    public class DoctorDto
    {
        public string Biography { get; set; }
        public decimal ConsultFee { get; set; }
        public int Experience { get; set; }

        public SpecialityDto Speciality { get; set; }
        public IEnumerable<LanguageDto> Languages { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Doctor, DoctorDto>();
            }
        }
    }
}
