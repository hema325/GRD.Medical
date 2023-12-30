using Application.Common.Models;

namespace Application.Account.Commands.UploadAccountImage
{
    internal class UploadAccountImageCommandHandler : IRequestHandler<UploadAccountImageCommand, UploadAccountImageCommandDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFileStorage _fileStorage;
        private readonly ICurrentUser _currentUser;
        private readonly ICurrentHttpRequest _httpRequest;

        public UploadAccountImageCommandHandler(IApplicationDbContext context,
            IFileStorage fileStorage,
            ICurrentUser currentUser,
            ICurrentHttpRequest httpRequest)
        {
            _context = context;
            _fileStorage = fileStorage;
            _currentUser = currentUser;
            _httpRequest = httpRequest;
        }

        public async Task<UploadAccountImageCommandDto> Handle(UploadAccountImageCommand request, CancellationToken cancellationToken)
        {
            var user = await  _context.Users.FindAsync(_currentUser.Id);

            if (user == null)
                throw new NotFoundException(nameof(User));

            if (user.ImageUrl != null)
                await _fileStorage.RemoveAsync(user.ImageUrl);

            user.ImageUrl = await _fileStorage.SaveAsync(request.Image);

            await _context.SaveChangesAsync();

            return new UploadAccountImageCommandDto
            { 
                Url = $"{_httpRequest.Scheme}://{_httpRequest.Host}/{user.ImageUrl}" 
            };

        }
    }
}
