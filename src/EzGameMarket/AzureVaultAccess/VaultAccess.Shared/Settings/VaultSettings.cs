namespace VaultAccess.Shared.Settings
{
    public class VaultSettings : IVaultSettings
    {
        public bool UseVault { get; set; }
        public Vault Vault { get; set; }
    }
}