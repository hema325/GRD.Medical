using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MedicalAdvices.Queries.GetMedicalAdviceByID
{
    public class GetMedicalAdviceByIdQuary : IRequest<MedicalAdviceDto>
    {
        public int Id { get; set; }
    }
}
