namespace NovoSpas.API.Helpers
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public string ActiveDirectoryDomain { get; set; }
        public string ActiveDirectoryDomainSecondary { get; set; }
        public string ActiveDirectoryPort { get; set; }
        public string ActiveDirectoryFieldLogin { get; set; }
        public string ActiveDirectoryFieldName { get; set; }
        public string ApiUsername { get; set; }
        public string ApiPassword { get; set; }
    }

}