namespace Application.Account.Commands
{
    public class AuthResultDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string? ImageUrl { get; set; }
        public string JWTToken { get; set; }
        public DateTime JWTTokenExpiresOn { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiresOn { get; set; }
    }
}
