using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MedicalAdvices.Commands.UpdateMedicalAdvice
{
    internal class UpdateMedicalAdviceCommandHandler : IRequestHandler<UpdateMedicalAdviceCommand>
    {
        private readonly IFileStorage _fileStorage;
        private readonly IApplicationDbContext _context;

        public UpdateMedicalAdviceCommandHandler(IFileStorage fileStorage, IApplicationDbContext context)
        {
            _fileStorage = fileStorage;
            _context = context;
        }

        public async Task<Unit> Handle(UpdateMedicalAdviceCommand request, CancellationToken cancellationToken)
        {
            var medicaladvice = await _context.MedicalAdvices.FindAsync(request.Id);
            if (medicaladvice == null)
                throw new NotFoundException(nameof(medicaladvice));

            medicaladvice.Title = request.Title;
            medicaladvice.Content = request.Content;
            medicaladvice.AuthorId = request.AuthorId;
            if (request.Image != null)
            {
                await _fileStorage.RemoveAsync(medicaladvice.ImageUrl);
                medicaladvice.ImageUrl = await _fileStorage.SaveAsync(request.Image);
            }
            medicaladvice.AddDomainEvent(new EntityUpdatedEvent(medicaladvice));
            await _context.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
