using Application.Comments.Queries;
using Domain.Entities.OwnedEntities;

namespace Application.Comments.Commands.CreateComment
{
    internal class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, CommentDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFileStorage _fileStorage;
        private readonly ICurrentUser _currentUser;
        private readonly IMapper _mapper;

        public CreateCommentCommandHandler(IApplicationDbContext context,
                                           IFileStorage fileStorage,
                                           ICurrentUser currentUser,
                                           IMapper mapper)
        {
            _context = context;
            _fileStorage = fileStorage;
            _currentUser = currentUser;
            _mapper = mapper;
        }

        public async Task<CommentDto> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = new Comment
            {
                CommentedOn = DateTime.UtcNow,
                Content = request.Content,
                OwnerId = _currentUser.Id.Value,
                ReplyTo = request.ReplyTo,
                PostId = request.PostId
            };

            if(request.Image != null)
                comment.Media = new Media
                {
                    Type = MediaTypes.Image,
                    Url = await _fileStorage.SaveAsync(request.Image)
                };


            comment.AddDomainEvent(new CommentCreatedEvent(comment));
            comment.AddDomainEvent(new EntityCreatedEvent(comment));

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            comment.Owner = await _context.Users.FindAsync(_currentUser.Id);
            return _mapper.Map<CommentDto>(comment);
        }
    }
}
