using Infrastructure.Persistance.Migrations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance.Seeds
{
    internal static class ModelSeeder
    {
        public static void Seed(ModelBuilder builder)
        {
            Func<DateTime> GetRandomDateTime = () =>
            {
                var random = new Random();
                return DateTime.UtcNow.AddYears(-random.Next(0, 3)).AddMonths(-random.Next(0, 12)).AddDays(-random.Next(0, 30));
            };

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
                    ImageUrl = "Files/Seeds/Articles/TonsillitisInChildren.jpg",
                    AuthorId = authors[0].Id,
                    PublishedOn = GetRandomDateTime(),
                    Content = File.ReadAllText("..\\Infrastructure\\Persistance\\Seeds\\Articles\\Tonsillitis.txt")
                },
                new Article
                {
                    Id = 2,
                    Title = "Does stress make skin problems worse?",
                    ImageUrl = "Files/Seeds/Articles/Stress.jpg",
                    AuthorId = authors[1].Id,
                    PublishedOn = GetRandomDateTime(),
                    Content = File.ReadAllText("..\\Infrastructure\\Persistance\\Seeds\\Articles\\Stress.txt")
                },
                new Article
                {
                    Id = 3,
                    Title = "Having a heart attack when you're young",
                    ImageUrl = "Files/Seeds/Articles/HeartAttack.webp",
                    AuthorId = authors[2].Id,
                    PublishedOn = GetRandomDateTime(),
                    Content = File.ReadAllText("..\\Infrastructure\\Persistance\\Seeds\\Articles\\HeartAttack.txt")
                },
                new Article
                {
                    Id = 4,
                    Title = "Red spots? Fleshy bumps? When to worry about spots on the penis",
                    ImageUrl = "Files/Seeds/Articles/Spots.jpeg",
                    AuthorId = authors[3].Id,
                    PublishedOn = GetRandomDateTime(),
                    Content = File.ReadAllText("..\\Infrastructure\\Persistance\\Seeds\\Articles\\Spots.txt")
                },
                new Article
                {
                    Id = 5,
                    Title = "The best and the worst times of year to get pregnant",
                    ImageUrl = "Files/Seeds/Articles/GetPregnant.jpeg",
                    AuthorId = authors[4].Id,
                    PublishedOn = GetRandomDateTime(),
                    Content = File.ReadAllText("..\\Infrastructure\\Persistance\\Seeds\\Articles\\GetPregnant.txt")
                },
                new Article
                {
                    Id = 6,
                    Title = "How long before you travel should you get vaccinated?",
                    ImageUrl = "Files/Seeds/Articles/GetVaccinated.webp",
                    AuthorId = authors[5].Id,
                    PublishedOn = GetRandomDateTime(),
                    Content = File.ReadAllText("..\\Infrastructure\\Persistance\\Seeds\\Articles\\GetPregnant.txt")
                },
                new Article
                {
                    Id = 7,
                    Title = "How to find the right treatment for your hay fever",
                    ImageUrl = "Files/Seeds/Articles/Fever.webp",
                    AuthorId = authors[6].Id,
                    PublishedOn = GetRandomDateTime(),
                    Content = File.ReadAllText("..\\Infrastructure\\Persistance\\Seeds\\Articles\\Fever.txt")
                },
                new Article
                {
                    Id = 8,
                    Title = "Can changing your diet ease endometriosis symptoms?",
                    ImageUrl = "Files/Seeds/Articles/Diet.jpg",
                    AuthorId = authors[7].Id,
                    PublishedOn = GetRandomDateTime(),
                    Content = File.ReadAllText("..\\Infrastructure\\Persistance\\Seeds\\Articles\\Diet.txt")
                },
                new Article
                {
                    Id = 9,
                    Title = "Is your cough really a chest infection?",
                    ImageUrl = "Files/Seeds/Articles/Cough.jpg",
                    AuthorId = authors[8].Id,
                    PublishedOn = GetRandomDateTime(),
                    Content = File.ReadAllText("..\\Infrastructure\\Persistance\\Seeds\\Articles\\Cough.txt")
                },
                new Article
                {
                    Id = 10,
                    Title = "Cancer symptoms you should never ignore",
                    ImageUrl = "Files/Seeds/Articles/Cancer.jpeg",
                    AuthorId = authors[9].Id,
                    PublishedOn = GetRandomDateTime(),
                    Content = File.ReadAllText("..\\Infrastructure\\Persistance\\Seeds\\Articles\\Cancer.txt")
                },
                new Article
                {
                    Id = 11,
                    Title = "Reasons you feel tired all the time",
                    ImageUrl = "Files/Seeds/Articles/FeelingTired.jpeg",
                    AuthorId = authors[6].Id,
                    PublishedOn = GetRandomDateTime(),
                    Content = File.ReadAllText("..\\Infrastructure\\Persistance\\Seeds\\Articles\\FeelingTired.txt")
                }
                ,new Article
                {
                    Id = 12,
                    Title = "What's causing your chest pain?",
                    ImageUrl = "Files/Seeds/Articles/ChestPain.webp",
                    AuthorId = authors[10].Id,
                    PublishedOn = GetRandomDateTime(),
                    Content = File.ReadAllText("..\\Infrastructure\\Persistance\\Seeds\\Articles\\ChestPain.txt")
                }
            };

            builder.Entity<Article>().HasData(articles);

            var advices = new[] {
                new Advice {
                    Id = 1,
                    Title = "5 Simple Rules for Amazing Health",
                    ImageUrl = "Files/Seeds/Advices/AmazingHealth.webp",
                    AuthorId = authors[0].Id,
                    PublishedOn = GetRandomDateTime(),
                    Content = File.ReadAllText("..\\Infrastructure\\Persistance\\Seeds\\Advices\\AmazingHealth.txt")
                },
                new Advice {
                    Id = 2,
                    Title = "12 Common Food Additives — Should You Avoid Them?",
                    ImageUrl = "Files/Seeds/Advices/FoodAdditives.jpeg",
                    AuthorId = authors[1].Id,
                    PublishedOn = GetRandomDateTime(),
                    Content = File.ReadAllText("..\\Infrastructure\\Persistance\\Seeds\\Advices\\FoodAdditives.txt")
                },
                new Advice {
                    Id = 3,
                    Title = "Mental Health, Depression, and MenopauseMental Health, Depression, and Menopause",
                    ImageUrl = "Files/Seeds/Advices/MentalHealth.jpeg",
                    AuthorId = authors[2].Id,
                    PublishedOn = GetRandomDateTime(),
                    Content = File.ReadAllText("..\\Infrastructure\\Persistance\\Seeds\\Advices\\MentalHealth.txt")
                },
                new Advice {
                    Id = 4,
                    Title = "10 Encouraging Signs of Progress on Your Weight Loss Journey",
                    ImageUrl = "Files/Seeds/Advices/WeightLoss.jpg",
                    AuthorId = authors[3].Id,
                    PublishedOn = GetRandomDateTime(),
                    Content = File.ReadAllText("..\\Infrastructure\\Persistance\\Seeds\\Advices\\WeightLoss.txt")
                },
                new Advice {
                    Id = 5,
                    Title = "Top 12 Biggest Myths About Weight Loss",
                    ImageUrl = "Files/Seeds/Advices/MythsAboutWeightLoss.jpeg",
                    AuthorId = authors[4].Id,
                    PublishedOn = GetRandomDateTime(),
                    Content = File.ReadAllText("..\\Infrastructure\\Persistance\\Seeds\\Advices\\MythsAboutWeightLoss.txt")
                },
                new Advice {
                    Id = 6,
                    Title = "8 Best Personalized Vitamin Subscription Services of 2023, According to Dietitians",
                    ImageUrl = "Files/Seeds/Advices/VitaminBrands.jpeg",
                    AuthorId = authors[5].Id,
                    PublishedOn = GetRandomDateTime(),
                    Content = File.ReadAllText("..\\Infrastructure\\Persistance\\Seeds\\Advices\\VitaminBrands.txt")
                },
                new Advice {
                    Id = 7,
                    Title = "12 Tips for a Speedy Flu Recovery",
                    ImageUrl = "Files/Seeds/Advices/SpeedyFluRecovery.webp",
                    AuthorId = authors[6].Id,
                    PublishedOn = GetRandomDateTime(),
                    Content = File.ReadAllText("..\\Infrastructure\\Persistance\\Seeds\\Advices\\SpeedyFluRecovery.txt")
                },
                new Advice {
                    Id = 8,
                    Title = "Habits to Form Now for a Longer Life",
                    ImageUrl = "Files/Seeds/Advices/LongerLife.webp",
                    AuthorId = authors[7].Id,
                    PublishedOn = GetRandomDateTime(),
                    Content = File.ReadAllText("..\\Infrastructure\\Persistance\\Seeds\\Advices\\LongerLife.txt")
                },
                new Advice {
                    Id = 9,
                    Title = "Tips To Sleep Better",
                    ImageUrl = "Files/Seeds/Advices/SleepBetter.jpeg",
                    AuthorId = authors[8].Id,
                    PublishedOn = GetRandomDateTime(),
                    Content = File.ReadAllText("..\\Infrastructure\\Persistance\\Seeds\\Advices\\SleepBetter.txt")
                },
                new Advice {
                    Id = 10,
                    Title = "How to Lower Your Risk of Prostate Cancer",
                    ImageUrl = "Files/Seeds/Advices/ProstateCancer.jpg",
                    AuthorId = authors[9].Id,
                    PublishedOn = GetRandomDateTime(),
                    Content = File.ReadAllText("..\\Infrastructure\\Persistance\\Seeds\\Advices\\ProstateCancer.txt")
                },
                new Advice {
                    Id = 11,
                    Title = "How to Prevent Obesity in Kids and Adults",
                    ImageUrl = "Files/Seeds/Advices/PreventObesity.jpg",
                    AuthorId = authors[10].Id,
                    PublishedOn = GetRandomDateTime(),
                    Content = File.ReadAllText("..\\Infrastructure\\Persistance\\Seeds\\Advices\\PreventObesity.txt")
                },
                new Advice {
                    Id = 12,
                    Title = "14 Best Natural Cough Remedies and Prevention Tips",
                    ImageUrl = "Files/Seeds/Advices/Cough.jpg",
                    AuthorId = authors[6].Id,
                    PublishedOn = GetRandomDateTime(),
                    Content = File.ReadAllText("..\\Infrastructure\\Persistance\\Seeds\\Advices\\Cough.txt")
                }
            };

            builder.Entity<Advice>().HasData(advices);

            var languages = new[]
            {
                new Language { Id = 1, Name="Arabic" },
                new Language { Id = 2, Name="English" },
                new Language { Id = 3, Name="Spanish" },
                new Language { Id = 4, Name="French" },
                new Language { Id = 5, Name="German" },
                new Language { Id = 6, Name="Italian" },
                new Language { Id = 7, Name="Russian" },
                new Language { Id = 8, Name="Bengali" },
                new Language { Id = 9, Name="Portuguese" },
                new Language { Id = 10, Name="Indonesian" },
                new Language { Id = 11, Name="Chinese" },
                new Language { Id = 12, Name="Hindi" }
            };

            builder.Entity<Language>().HasData(languages);

            var specialists = new[]
            {
                new Speciality { Id = 1, Name = "Breast", ImageUrl = "Files/Seeds/Specialists/breast.png"},
                new Speciality { Id = 2, Name = "Obstetrics & Gynaecology", ImageUrl = "Files/Seeds/Specialists/Obgyn.png"},
                new Speciality { Id = 3, Name = "Children", ImageUrl = "Files/Seeds/Specialists/children.png"},
                new Speciality { Id = 4, Name = "Colorectal & Rectum", ImageUrl = "Files/Seeds/Specialists/intestines.png"},
                new Speciality { Id = 5, Name = "Ear, Nose & Throat", ImageUrl = "Files/Seeds/Specialists/nasal.png"},
                new Speciality { Id = 6, Name = "Gall Bladder, Liver, Pancreas & Stomach", ImageUrl = "Files/Seeds/Specialists/gallbladder.png"},
                new Speciality { Id = 7, Name = "Heart", ImageUrl = "Files/Seeds/Specialists/heart.png"},
                new Speciality { Id = 8, Name = "Blood Vessels", ImageUrl = "Files/Seeds/Specialists/vascularSurgery.png"},
                new Speciality { Id = 9, Name = "Joints & Bones", ImageUrl = "Files/Seeds/Specialists/shoulder.png"},
                new Speciality { Id = 10, Name = "Kidney", ImageUrl = "Files/Seeds/Specialists/kidney.png"},
                new Speciality { Id = 11, Name = "Stomach & Digestive System", ImageUrl = "Files/Seeds/Specialists/stomach.png"},
                new Speciality { Id = 12, Name = "Urinary & Reproductive System", ImageUrl = "Files/Seeds/Specialists/bladder.png"},
            };

            builder.Entity<Speciality>().HasData(specialists);
        }
    }
}
