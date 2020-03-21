using System;
using System.Collections.Generic;
using System.Text;

namespace VaultAccess.Shared.Settings
{
    public interface IVaultSettings
    {
        public bool UseVault { get; set; }

        public Vault Vault { get; set; }
    }
}
