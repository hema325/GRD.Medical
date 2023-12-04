﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Articles.Commands.UpdateArticle
{
    public class UpdateArticleCommandHandler : IRequestHandler<UpdateArticleCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFileStorage _fileStorage;

        public UpdateArticleCommandHandler(IApplicationDbContext context, IFileStorage fileStorage)
        {
            _context = context;
            _fileStorage = fileStorage;
        }

        public async Task<Unit> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
        {
           var article = await _context.Articles.FindAsync(request.Id);
            if (article == null) 
                throw new NotFoundException(nameof(article));
            
            article.Title = request.Title;
            article.PublicationDate = request.PublicationDate;
            article.Content = request.Content;
            article.CategoryId = request.categoryId;
            article.AuthorId = request.AuthorId;
            if(request.Image  != null)
            {
                await _fileStorage.RemoveAsync(article.ImageUrl);
                article.ImageUrl = await _fileStorage.SaveAsync(request.Image);
            }
            article.AddDomainEvent(new EntityUpdatedEvent(article));
            await _context.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
