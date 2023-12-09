using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance.Seeds
{
    internal static class ModelSeeder
    {
        public static void Seed(ModelBuilder builder)
        {
            var random = new Random();

            var authors = new[] {
                new Author { Id = 1, Name = "Lydia Smith" },
                new Author { Id = 2, Name =  "Léa Surugue" },
                new Author { Id = 3, Name = "Dr Jennifer Kelly" },
                new Author { Id = 4, Name = "Julian Turner" },
            };

            builder.Entity<Author>().HasData(authors);

            var articles = new[]
            {
                new Article
                {
                    Id = 1,
                    Title = "How to manage tonsillitis in children",
                    ImageUrl = "Files/images/TonsillitisInChildren.jpg",
                    AuthorId = authors[0].Id,
                    PublishedOn = DateTime.UtcNow.AddYears(-random.Next(0,5)).AddMonths(-random.Next(0,12)).AddDays(-random.Next(0,30)),
                    Content = File.ReadAllText("..\\Infrastructure\\Persistance\\Seeds\\Articles\\Tonsillitis.txt")
                },
                new Article
                {
                    Id = 2,
                    Title = "Does stress make skin problems worse?",
                    ImageUrl = "Files/images/Stress.jpg",
                    AuthorId = authors[1].Id,
                    PublishedOn = DateTime.UtcNow.AddYears(-random.Next(0,5)).AddMonths(-random.Next(0,12)).AddDays(-random.Next(0,30)),
                    Content = File.ReadAllText("..\\Infrastructure\\Persistance\\Seeds\\Articles\\Stress.txt")
                },
                new Article
                {
                    Id = 3,
                    Title = "Having a heart attack when you're young",
                    ImageUrl = "Files/images/HeartAttack.webp",
                    AuthorId = authors[2].Id,
                    PublishedOn = DateTime.UtcNow.AddYears(-random.Next(0,5)).AddMonths(-random.Next(0,12)).AddDays(-random.Next(0,30)),
                    Content = File.ReadAllText("..\\Infrastructure\\Persistance\\Seeds\\Articles\\HeartAttack.txt")
                }
                , new Article
                {
                    Id = 4,
                    Title = "Red spots? Fleshy bumps? When to worry about spots on the penis",
                    ImageUrl = "Files/images/Spots.webp",
                    AuthorId = authors[3].Id,
                    PublishedOn = DateTime.UtcNow.AddYears(-random.Next(0,5)).AddMonths(-random.Next(0,12)).AddDays(-random.Next(0,30)),
                    Content = File.ReadAllText("..\\Infrastructure\\Persistance\\Seeds\\Articles\\Spots.txt")
                },

            };

            builder.Entity<Article>().HasData(articles);
        }
    }
}
