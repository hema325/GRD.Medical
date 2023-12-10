namespace Application.MedicalAdvices.Commands.DeleteMedicalAdvice
{
    internal class DeleteAdviceCommandHandler : IRequestHandler<DeleteAdviceCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFileStorage _fileStorage;

        public DeleteAdviceCommandHandler(IApplicationDbContext context, IFileStorage fileStorage)
        {
            _context = context;
            _fileStorage = fileStorage;
        }

        public async Task<Unit> Handle(DeleteAdviceCommand request, CancellationToken cancellationToken)
        {
            var advice = await _context.Advices.FindAsync(request.Id);

            if (advice == null)
                throw new NotFoundException(nameof(Advice));

            advice.AddDomainEvent(new EntityDeletedEvent(advice));

            await _fileStorage.RemoveAsync(advice.ImageUrl);
            _context.Advices.Remove(advice);
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
