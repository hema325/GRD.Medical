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
                new Author { Id = 5, Name = "Sara Lindberg" },
                new Author { Id = 6, Name = "Julian Turne" },
                new Author { Id = 7, Name = "Abi Millar" },
                new Author { Id = 8, Name = "Natalie Healey" },
                new Author { Id = 9, Name = "Gillian Harvey" },
                new Author { Id = 10, Name = "Dr Sarah Jarvis MBE" },
                new Author { Id = 11, Name = "Glynis Kozma " },
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
                },
                new Article
                {
                    Id = 4,
                    Title = "Red spots? Fleshy bumps? When to worry about spots on the penis",
                    ImageUrl = "Files/images/Spots.webp",
                    AuthorId = authors[3].Id,
                    PublishedOn = DateTime.UtcNow.AddYears(-random.Next(0,5)).AddMonths(-random.Next(0,12)).AddDays(-random.Next(0,30)),
                    Content = File.ReadAllText("..\\Infrastructure\\Persistance\\Seeds\\Articles\\Spots.txt")
                },
                new Article
                {
                    Id = 5,
                    Title = "The best and the worst times of year to get pregnant",
                    ImageUrl = "Files/images/GetPregnant.webp",
                    AuthorId = authors[4].Id,
                    PublishedOn = DateTime.UtcNow.AddYears(-random.Next(0,5)).AddMonths(-random.Next(0,12)).AddDays(-random.Next(0,30)),
                    Content = File.ReadAllText("..\\Infrastructure\\Persistance\\Seeds\\Articles\\GetPregnant.txt")
                },
                new Article
                {
                    Id = 6,
                    Title = "How long before you travel should you get vaccinated?",
                    ImageUrl = "Files/images/Travel.jpg",
                    AuthorId = authors[5].Id,
                    PublishedOn = DateTime.UtcNow.AddYears(-random.Next(0,5)).AddMonths(-random.Next(0,12)).AddDays(-random.Next(0,30)),
                    Content = File.ReadAllText("..\\Infrastructure\\Persistance\\Seeds\\Articles\\GetPregnant.txt")
                },
                new Article
                {
                    Id = 7,
                    Title = "How to find the right treatment for your hay fever",
                    ImageUrl = "Files/images/Fever.webp",
                    AuthorId = authors[6].Id,
                    PublishedOn = DateTime.UtcNow.AddYears(-random.Next(0,5)).AddMonths(-random.Next(0,12)).AddDays(-random.Next(0,30)),
                    Content = File.ReadAllText("..\\Infrastructure\\Persistance\\Seeds\\Articles\\Fever.txt")
                },
                new Article
                {
                    Id = 8,
                    Title = "Can changing your diet ease endometriosis symptoms?",
                    ImageUrl = "Files/images/Diet.webp",
                    AuthorId = authors[7].Id,
                    PublishedOn = DateTime.UtcNow.AddYears(-random.Next(0,5)).AddMonths(-random.Next(0,12)).AddDays(-random.Next(0,30)),
                    Content = File.ReadAllText("..\\Infrastructure\\Persistance\\Seeds\\Articles\\Diet.txt")
                },
                new Article
                {
                    Id = 9,
                    Title = "Is your cough really a chest infection?",
                    ImageUrl = "Files/images/Cough.jpg",
                    AuthorId = authors[8].Id,
                    PublishedOn = DateTime.UtcNow.AddYears(-random.Next(0,5)).AddMonths(-random.Next(0,12)).AddDays(-random.Next(0,30)),
                    Content = File.ReadAllText("..\\Infrastructure\\Persistance\\Seeds\\Articles\\Cough.txt")
                },
                new Article
                {
                    Id = 10,
                    Title = "Cancer symptoms you should never ignore",
                    ImageUrl = "Files/images/Cancer.jpg",
                    AuthorId = authors[9].Id,
                    PublishedOn = DateTime.UtcNow.AddYears(-random.Next(0,5)).AddMonths(-random.Next(0,12)).AddDays(-random.Next(0,30)),
                    Content = File.ReadAllText("..\\Infrastructure\\Persistance\\Seeds\\Articles\\Cancer.txt")
                },
                new Article
                {
                    Id = 11,
                    Title = "Reasons you feel tired all the time",
                    ImageUrl = "Files/images/FeelingTired.webp",
                    AuthorId = authors[6].Id,
                    PublishedOn = DateTime.UtcNow.AddYears(-random.Next(0,5)).AddMonths(-random.Next(0,12)).AddDays(-random.Next(0,30)),
                    Content = File.ReadAllText("..\\Infrastructure\\Persistance\\Seeds\\Articles\\FeelingTired.txt")
                }
                ,new Article
                {
                    Id = 12,
                    Title = "What's causing your chest pain?",
                    ImageUrl = "Files/images/ChestPain.webp",
                    AuthorId = authors[10].Id,
                    PublishedOn = DateTime.UtcNow.AddYears(-random.Next(0,5)).AddMonths(-random.Next(0,12)).AddDays(-random.Next(0,30)),
                    Content = File.ReadAllText("..\\Infrastructure\\Persistance\\Seeds\\Articles\\ChestPain.txt")
                }
            };

            builder.Entity<Article>().HasData(articles);
        }
    }
}
