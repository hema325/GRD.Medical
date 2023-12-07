namespace Application.Authors.Queries
{
    public class AuthorDto
    {
        public string Name { get; set; }

        private class Mapping: Profile
        {
            public Mapping()
            {
                CreateMap<Author, AuthorDto>();
            }
        }
    }
}
