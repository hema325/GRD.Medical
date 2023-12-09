namespace Application.Authors.Commands.DeleteAuthor
{
    internal class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFileStorage _fileStorage;

        public DeleteAuthorCommandHandler(IApplicationDbContext context, IFileStorage fileStorage)
        {
            _context = context;
            _fileStorage = fileStorage;
        }

        public async Task<Unit> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await _context.Authors.FindAsync(request.Id);

            if (author == null)
                throw new NotFoundException(nameof(Author));

            author.AddDomainEvent(new EntityDeletedEvent(author));

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
