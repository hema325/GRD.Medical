namespace Application.Authors.Commands.CreateAuthor
{
    internal class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFileStorage _fileStorage;

        public CreateAuthorCommandHandler(IApplicationDbContext context, IFileStorage fileStorage)
        {
            _context = context;
            _fileStorage = fileStorage;
        }

        public async Task<int> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = new Author
            {
                Name = request.Name,
                JobTitle = request.JobTitle,
                ImageUrl = await _fileStorage.SaveAsync(request.Image)
            };

            author.AddDomainEvent(new EntityCreatedEvent(author));
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return author.Id;
        }
    }
}
