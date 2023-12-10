using Application.MedicalAdvices.Commands.UpdateMedicalAdvice;

namespace Application.advices.Commands.Updateadvice
{
    internal class UpdateAdviceCommandHandler : IRequestHandler<UpdateAdviceCommand>
    {
        private readonly IFileStorage _fileStorage;
        private readonly IApplicationDbContext _context;

        public UpdateAdviceCommandHandler(IFileStorage fileStorage, IApplicationDbContext context)
        {
            _fileStorage = fileStorage;
            _context = context;
        }

        public async Task<Unit> Handle(UpdateAdviceCommand request, CancellationToken cancellationToken)
        {
            var advice = await _context.Advices.FindAsync(request.Id);

            if (advice == null)
                throw new NotFoundException(nameof(advice));

            advice.Title = request.Title;
            advice.Content = request.Content;
            advice.AuthorId = request.AuthorId;
            advice.PublishedON = request.PublishedON;

            if (request.Image != null)
            {
                await _fileStorage.RemoveAsync(advice.ImageUrl);
                advice.ImageUrl = await _fileStorage.SaveAsync(request.Image);
            }

            advice.AddDomainEvent(new EntityUpdatedEvent(advice));
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
