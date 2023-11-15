namespace Infrastructure.Authentication.JWT
{
    internal class JWTToken
    {
        public string Token { get; set; }
        public DateTime ExpiresOn { get; set; }
    }
}
