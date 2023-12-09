namespace Application.Authors.Commands.CreateAuthor
{
    internal class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, int>
    {
        private readonly IApplicationDbContext _context;


        public CreateAuthorCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = new Author
            {
                Name = request.Name
            };

            author.AddDomainEvent(new EntityCreatedEvent(author));
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return author.Id;
        }
    }
}
