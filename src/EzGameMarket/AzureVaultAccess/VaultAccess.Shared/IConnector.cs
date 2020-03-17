using System;
using System.Collections.Generic;
using System.Text;

namespace VaultAccess.Shared
{
    public interface IConnector
    {
        IVaultSettings VaultSettings { get; }
        void Connect(IVaultSettings settings);
    }
}
