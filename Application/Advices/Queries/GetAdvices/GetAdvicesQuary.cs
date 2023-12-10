﻿using Application.Common.Models;

namespace Application.MedicalAdvices.Queries.GetMedicalAdvices
{
    public class GetAdvicesQuary : IRequest<PaginatedList<MedicalAdviceDto>>
    {
        public string? Title { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}