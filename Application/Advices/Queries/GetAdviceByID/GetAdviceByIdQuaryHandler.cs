using Microsoft.EntityFrameworkCore;

namespace Application.MedicalAdvices.Queries.GetMedicalAdviceByID
{
    internal class GetAdviceByIdQuaryHandler : IRequestHandler<GetAdviceByIdQuary, AdviceDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAdviceByIdQuaryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AdviceDto> Handle(GetAdviceByIdQuary request, CancellationToken cancellationToken)
        {
            var advice = await _context.Advices.Include(a => a.Author).FirstOrDefaultAsync(a => a.Id == request.Id);
            
            if (advice == null)
                throw new NotFoundException(nameof(advice));

            return _mapper.Map<AdviceDto>(advice);
        }
    }
}
