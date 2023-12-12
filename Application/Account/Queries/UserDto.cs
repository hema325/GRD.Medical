using Application.Common.Mappings;

namespace Application.Account.Queries
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime JoinedOn { get; set; }


        private class Mapping: Profile
        {
            public Mapping()
            {
                CreateMap<User, UserDto>()
                    .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()))
                    .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom<ImageUrlResolver>());
            }
        }
    }
}
