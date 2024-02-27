using Application.Appointments.Queries;
using Application.Articles.Queries;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MedicalInformation.Queries.GetMedicalInfo
{
    public class GetMedicalInfoQueryHandler : IRequestHandler<GetMedicalInfoQuery, MedicalInfoDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUser _currentUser;
        private readonly IMapper _mapper;

        public GetMedicalInfoQueryHandler(IApplicationDbContext context, ICurrentUser currentUser, IMapper mapper)
        {
            _context = context;
            _currentUser = currentUser;
            _mapper = mapper;
        }

        public async Task<MedicalInfoDto> Handle(GetMedicalInfoQuery request, CancellationToken cancellationToken)
        {

            var medicalInfo = await _context.MedicalInfos.FirstOrDefaultAsync(u => u.UserId ==_currentUser.Id);


            if (medicalInfo == null)
                throw new NotFoundException(nameof(MedicalInfo));

            return _mapper.Map<MedicalInfoDto>(medicalInfo);
        }
    }
}
