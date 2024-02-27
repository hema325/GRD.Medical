using Application.Notifications.Queries;
using Application.Users.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MedicalInformation.Queries
{
    public class MedicalInfoDto
    {
        public int Age { get; set; }
        public float Hight { get; set; }
        public float Wight { get; set; }
        public bool Diabetic { get; set; }
        public bool Hypertension { get; set; }
        public bool Hypotension { get; set; }
        public bool Smoker { get; set; }
        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<MedicalInfo, MedicalInfoDto>();
            }
        }
    }
}
