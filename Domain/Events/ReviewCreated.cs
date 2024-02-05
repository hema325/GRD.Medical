using Domain.Entities;

namespace Domain.Events
{
    public class ReviewCreated: EventBase
    {
        public Review Review { get; }

        public ReviewCreated(Review review)
        {
            Review = review;
        }
    }
}
