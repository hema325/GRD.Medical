using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MedicalAdvices.Commands.DeleteMedicalAdvice
{
    internal class DeleteMedicalAdviceCommandHandler : IRequestHandler<DeleteMedicalAdviceCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFileStorage _fileStorage;

        public DeleteMedicalAdviceCommandHandler(IApplicationDbContext context, IFileStorage fileStorage)
        {
            _context = context;
            _fileStorage = fileStorage;
        }

        public async Task<Unit> Handle(DeleteMedicalAdviceCommand request, CancellationToken cancellationToken)
        {
            var medicaladvice = await _context.MedicalAdvices.FindAsync(request.Id);
            if (medicaladvice == null)
            {
                throw new NotFoundException(nameof(MedicalAdvice));
            }

            medicaladvice.AddDomainEvent(new EntityDeletedEvent(medicaladvice));
            await _fileStorage.RemoveAsync(medicaladvice.ImageUrl);
            _context.MedicalAdvices.Remove(medicaladvice);
            await _context.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
