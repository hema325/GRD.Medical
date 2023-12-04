using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Articles.Commands.CreateArticle
{
    public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFileStorage _fileStorage;

        public CreateArticleCommandHandler(IApplicationDbContext context, IFileStorage fileStorage)
        {
            _context = context;
            _fileStorage = fileStorage;
        }

        public async Task<int> Handle(CreateArticleCommand command, CancellationToken cancellationToken)
        {
            var article = new Article
            {
                Title = command.Title,
                PublicationDate = command.PublicationDate,
                Content = command.Content,
                ImageUrl = await _fileStorage.SaveAsync(command.Image),
                AuthorId = command.AuthorId,
                CategoryId = command.categoryId
            };
            article.AddDomainEvent(new EntityCreatedEvent(article));
            _context.Articles.Add(article);
            await _context.SaveChangesAsync();

            return article.Id;
        }
    }
}
