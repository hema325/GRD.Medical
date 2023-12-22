namespace Application.Posts.Commands.DeletePost
{
    internal class DeletePostCommandHandler : IRequestHandler<DeletePostCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFileStorage _fileStorage;
        private readonly ICurrentUser _currentUser;

        public DeletePostCommandHandler(IApplicationDbContext context, IFileStorage fileStorage, ICurrentUser currentUser)
        {
            _context = context;
            _fileStorage = fileStorage;
            _currentUser = currentUser;
        }

        public async Task<Unit> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            var post = await _context.Posts.FindAsync(request.Id);

            if (post == null)
                throw new NotFoundException(nameof(Post));

            if (post.OwnerId != _currentUser.Id && _currentUser.Role != Roles.Admin)
                throw new ForbiddenException("You are not the owner of this post");

            post.AddDomainEvent(new EntityDeletedEvent(post));
            await Task.WhenAll(post.Medias.Select(m => _fileStorage.RemoveAsync(m.Url)));
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
