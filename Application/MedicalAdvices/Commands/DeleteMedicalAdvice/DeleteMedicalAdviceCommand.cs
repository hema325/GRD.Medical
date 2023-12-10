using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MedicalAdvices.Commands.DeleteMedicalAdvice
{
    public class DeleteMedicalAdviceCommand:IRequest
    {
        public int Id { get; set; }
    }
}
