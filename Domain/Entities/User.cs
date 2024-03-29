﻿
namespace Domain.Entities
{
    public class User: EntityBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public string HashedPassword { get; set; }
        public Roles Role { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime JoinedOn { get; set; }

        public Doctor Doctor { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}
