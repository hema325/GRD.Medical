namespace Infrastructure.Cors
{
    internal class CorsSettings
    {
        public const string SectionName = "Cors";

        public string[] Origins { get; set; }
    }
}
