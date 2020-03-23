namespace VaultAccess.Shared.Settings
{
    public interface IVaultSettings
    {
        public bool UseVault { get; set; }

        public Vault Vault { get; set; }
    }
}