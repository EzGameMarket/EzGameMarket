using VaultAccess.Shared.Settings;

namespace VaultAccess.Shared.Abstractions
{
    public interface IConnector
    {
        IVaultSettings VaultSettings { get; }

        void Connect(IVaultSettings settings);
    }
}