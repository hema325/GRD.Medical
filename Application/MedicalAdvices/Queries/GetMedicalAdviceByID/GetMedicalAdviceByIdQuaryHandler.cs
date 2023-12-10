using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MedicalAdvices.Queries.GetMedicalAdviceByID
{
    internal class GetMedicalAdviceByIdQuaryHandler : IRequestHandler<GetMedicalAdviceByIdQuary, MedicalAdviceDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetMedicalAdviceByIdQuaryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MedicalAdviceDto> Handle(GetMedicalAdviceByIdQuary request, CancellationToken cancellationToken)
        {
            var medicaladvice = await _context.MedicalAdvices.Include(a => a.Author).FirstOrDefaultAsync(a => a.Id == request.Id);
            if (medicaladvice == null)
                throw new NotFoundException(nameof(medicaladvice));
            return _mapper.Map<MedicalAdviceDto>(medicaladvice);

        }
    }
}
