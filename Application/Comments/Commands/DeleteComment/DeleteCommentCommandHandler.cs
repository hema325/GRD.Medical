using Microsoft.EntityFrameworkCore;

namespace Application.Comments.Commands.DeleteComment
{
    internal class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUser _currentUser;
        private readonly IFileStorage _fileStorage;

        public DeleteCommentCommandHandler(IApplicationDbContext context,
                                           ICurrentUser currentUser,
                                           IFileStorage fileStorage)
        {
            _context = context;
            _currentUser = currentUser;
            _fileStorage = fileStorage;
        }

        public async Task<Unit> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = await _context.Comments
                .Include(c=>c.Replies)
                .FirstOrDefaultAsync(c=>c.Id == request.Id);

            if (comment == null)
                throw new NotFoundException(nameof(Comment));

            if(comment.OwnerId != _currentUser.Id && _currentUser.Role != Roles.Admin)
                throw new ForbiddenException("You are not the owner of this comment");

            //remove existing files
            if(comment.Media != null)
                await _fileStorage.RemoveAsync(comment.Media.Url);

            await Task.WhenAll(comment.Replies.Select(rply => rply.Media != null ? _fileStorage.RemoveAsync(rply.Media.Url): Task.CompletedTask));

            //remove comment with its replies from the db
            _context.Comments.Remove(comment);
            _context.Comments.RemoveRange(comment.Replies);
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
