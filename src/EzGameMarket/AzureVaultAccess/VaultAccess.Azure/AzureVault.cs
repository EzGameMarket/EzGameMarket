using System;
using VaultAccess.Shared.Abstractions;
using VaultAccess.Shared.Settings;

namespace VaultAccess.Azure
{
    public class AzureVault : IVaultAccessRepository
    {
        public AzureVault(IVaultSettings vaultSettings)
        {
            VaultSettings = vaultSettings ?? throw new ArgumentNullException($"A {nameof(vaultSettings)} nincs hozzáadva a dependency injection rendszerhez");
        }

        public IVaultSettings VaultSettings { get; private set; }

        public void Connect(IVaultSettings settings)
        {
        }

        public T GetByteData<T>(string key)
        {
            throw new NotImplementedException();
        }

        public string GetString(string key)
        {
            throw new NotImplementedException();
        }

        public void SetByteData<T>(string key, T data)
        {
            throw new NotImplementedException();
        }

        public void SetString(string key, string data)
        {
            throw new NotImplementedException();
        }
    }
}