namespace Application.MedicalAdvices.Commands.CretaeMedicalAdvice
{
    internal class CreateAdviceCommandHandler : IRequestHandler<CreateAdviceCommand, int>
    {
        private readonly IFileStorage _fileStorage;
        private readonly IApplicationDbContext _context;

        public CreateAdviceCommandHandler(IFileStorage fileStorage, IApplicationDbContext context)
        {
            _fileStorage = fileStorage;
            _context = context;
        }

        public async Task<int> Handle(CreateAdviceCommand request, CancellationToken cancellationToken)
        {
            var advice = new Advice
            {
                Title = request.Title,
                Content = request.Content,
                AuthorId = request.AuthorId,
                ImageUrl = await _fileStorage.SaveAsync(request.Image),
                PublishedOn = request.PublishedOn
            };

            advice.AddDomainEvent(new EntityCreatedEvent(advice));

            _context.Advices.Add(advice);
            await _context.SaveChangesAsync();

            return advice.Id;
        }

    }

}
