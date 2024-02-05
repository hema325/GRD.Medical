namespace Infrastructure.Authentication.Settings
{
    internal class ClientSettings: IClientSettings
    {
        public const string SectionName = "Client";
        public string BaseUrl { get; set; }
    }
}
