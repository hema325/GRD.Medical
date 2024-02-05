using Application.Users.Queries;

namespace Application.Reviews.Queries
{
    public class ReviewDto
    {
        public double Stars { get; set; }
        public string Content { get; set; }
        public DateTime ReviewedOn { get; set; }
        public UserDto Owner { get; set; }

        private class Mapping: Profile
        {
            public Mapping()
            {
                CreateMap<Review, ReviewDto>();
            }
        }
    }
}
