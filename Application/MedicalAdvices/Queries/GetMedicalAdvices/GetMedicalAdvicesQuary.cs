using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MedicalAdvices.Queries.GetMedicalAdvices
{
    public class GetMedicalAdvicesQuary : IRequest<PaginatedList<MedicalAdviceDto>>
    {
        public string? Title { get; set; }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
