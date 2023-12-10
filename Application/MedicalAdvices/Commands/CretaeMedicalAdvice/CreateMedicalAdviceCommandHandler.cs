using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MedicalAdvices.Commands.CretaeMedicalAdvice
{
    internal class CreateMedicalAdviceCommandHandler : IRequestHandler<CreateMedicalAdviceCommand, int>
    {
        private readonly IFileStorage _fileStorage;
        private readonly IApplicationDbContext _context;

        public CreateMedicalAdviceCommandHandler(IFileStorage fileStorage, IApplicationDbContext context)
        {
            _fileStorage = fileStorage;
            _context = context;
        }

        public async Task<int> Handle(CreateMedicalAdviceCommand request, CancellationToken cancellationToken)
        {
            var medicaladvice = new MedicalAdvice
            {
                Title = request.Title,
                Content = request.Content,
                AuthorId = request.AuthorId,
                ImageUrl = await _fileStorage.SaveAsync(request.Image),
                PublishedON = System.DateTime.Now,


            };
            medicaladvice.AddDomainEvent(new EntityCreatedEvent(medicaladvice));
            _context.MedicalAdvices.Add(medicaladvice);
            await _context.SaveChangesAsync();
            return medicaladvice.Id;
        }

    }

}
