namespace Infrastructure.Authentication.Settings
{
    internal class RefreshTokenSettings
    {
        public const string SectionName = "RefreshToken";

        public int Length { get; set; }
        public double ExpirationInDays { get; set; }
    }
}
