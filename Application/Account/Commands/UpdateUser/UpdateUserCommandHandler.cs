namespace Application.Account.Commands.UpdateUser
{
    internal class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUser _currentUser;

        public UpdateUserCommandHandler(IApplicationDbContext context, ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(_currentUser.Id);

            if (user == null)
                throw new NotFoundException(nameof(User));

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;

            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
