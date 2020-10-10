namespace VaultAccess.Shared.Settings
{
    public class Vault : IVault
    {
        public string Name { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}