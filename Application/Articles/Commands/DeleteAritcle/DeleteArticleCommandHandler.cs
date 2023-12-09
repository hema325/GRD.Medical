using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Articles.Commands.DeleteAritcle
{
    public class DeleteArticleCommandHandler : IRequestHandler<DeleteArticleCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFileStorage _fileStorage;

        public DeleteArticleCommandHandler(IApplicationDbContext context, IFileStorage fileStorage)
        {
            _context = context;
            _fileStorage = fileStorage;
        }

        public async Task<Unit> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
        {
            var article = await _context.Articles.FindAsync(request.Id);
            if (article == null)
            {
                throw new NotFoundException(nameof(Article));
            }
            article.AddDomainEvent(new EntityDeletedEvent(article));
            await _fileStorage.RemoveAsync(article.ImageUrl);
            _context.Articles.Remove(article);
            _context.SaveChangesAsync();
            return Unit.Value;

        }
    }
}
