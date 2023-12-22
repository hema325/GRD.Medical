using Application.Posts.Queries;
using Domain.OwnedEntities;

namespace Application.Posts.Commands.CreatePost
{
    internal class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, PostDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFileStorage _fileStorage;
        private readonly ICurrentUser _currentUser;
        private readonly IMapper _mapper;

        public CreatePostCommandHandler(IApplicationDbContext context,
                                        IFileStorage fileStorage,
                                        ICurrentUser currentUser,
                                        IMapper mapper)
        {
            _context = context;
            _fileStorage = fileStorage;
            _currentUser = currentUser;
            _mapper = mapper;
        }

        public async Task<PostDto> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var post = new Post
            {
                Content = request.Content,
                PostedOn = DateTime.UtcNow,
                OwnerId = _currentUser.Id!.Value
            };

            if (request.Images != null)
                post.Medias = (await Task.WhenAll(request.Images.Select(f => _fileStorage.SaveAsync(f))))
                .Select(url => new Media
                {
                    Type = MediaTypes.Image,
                    Url = url
                }).ToList();

            post.AddDomainEvent(new EntityCreatedEvent(post));
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            post.Owner = await _context.Users.FindAsync(_currentUser.Id);
            return _mapper.Map<PostDto>(post);
        }
    }
}
