namespace Application.Languages.Queries
{
    public class LanguageDto
    {
        public string Id { get; set; }
        public string Name { get; set; }

        private class Mapping: Profile
        {
            public Mapping()
            {
                CreateMap<Language, LanguageDto>();
            }
        }
    }
}
