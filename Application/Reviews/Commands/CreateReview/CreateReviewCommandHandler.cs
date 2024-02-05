using Microsoft.EntityFrameworkCore;

namespace Application.Reviews.Commands.CreateReview
{
    internal class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUser _currentUser;

        public CreateReviewCommandHandler(IApplicationDbContext context, ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<int> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            if (!await _context.Appointments.AnyAsync(a => a.PatientId == _currentUser.Id &&
            a.DoctorId == request.DoctorId &&
            DateTime.UtcNow >= a.EndDateTime))
                throw new ForbiddenException("You can't add a review if you don't have any previous appointments with this doctor.");

            var doctorId = await _context.Doctors
                .Where(d => d.UserId == request.DoctorId)
                .Select(d => d.Id)
                .FirstOrDefaultAsync();

            var review = new Review
            {
                Stars = request.Stars,
                Content = request.Content,
                DoctorId = doctorId,
                ReviewedOn = DateTime.UtcNow,
                OwnerId = _currentUser.Id.Value
            };

            review.AddDomainEvent(new ReviewCreated(review));
            review.AddDomainEvent(new EntityCreatedEvent(review));

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            return review.Id;
        }
    }
}
