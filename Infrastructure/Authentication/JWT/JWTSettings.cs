namespace Infrastructure.Authentication.JWT
{
    internal class JWTSettings
    {
        public const string SectionName = "JWT";

        public string Key { get; set; }
        public string Issuer { get; set; }
        public double ExpirationInMinutes { get; set; }
    }
}
