using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class MedicalInfo : EntityBase
    {
        public int Age {  get; set; }
        public float Hight { get; set; }
        public float Wight { get; set; }
        public bool Diabetic { get; set; }
        public bool Hypertension { get; set; }
        public bool Hypotension { get; set; }
        public bool Smoker { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
