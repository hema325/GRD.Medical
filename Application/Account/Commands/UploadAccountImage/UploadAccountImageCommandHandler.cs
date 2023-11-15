namespace Application.Account.Commands.UploadAccountImage
{
    internal class UploadAccountImageCommandHandler : IRequestHandler<UploadAccountImageCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFileStorage _fileStorage;
        private readonly ICurrentUser _currentUser;

        public UploadAccountImageCommandHandler(IApplicationDbContext context,
            IFileStorage fileStorage, 
            ICurrentUser currentUser)
        {
            _context = context;
            _fileStorage = fileStorage;
            _currentUser = currentUser;
        }

        public async Task<Unit> Handle(UploadAccountImageCommand request, CancellationToken cancellationToken)
        {
            var user = await  _context.Users.FindAsync(_currentUser.Id);

            if (user == null)
                throw new NotFoundException(nameof(User));

            if (user.ImageUrl != null)
                await _fileStorage.RemoveAsync(user.ImageUrl);

            user.ImageUrl = await _fileStorage.SaveAsync(request.Image);

            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
