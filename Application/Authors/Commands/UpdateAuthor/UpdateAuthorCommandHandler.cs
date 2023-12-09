namespace Application.Authors.Commands.UpdateAuthor
{
    internal class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFileStorage _fileStorage;

        public UpdateAuthorCommandHandler(IApplicationDbContext context, IFileStorage fileStorage)
        {
            _context = context;
            _fileStorage = fileStorage;
        }

        public async Task<Unit> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await _context.Authors.FindAsync(request.Id);

            if (author == null)
                throw new NotFoundException(nameof(Author));

            author.Name = request.Name;


            author.AddDomainEvent(new EntityUpdatedEvent(author));
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
