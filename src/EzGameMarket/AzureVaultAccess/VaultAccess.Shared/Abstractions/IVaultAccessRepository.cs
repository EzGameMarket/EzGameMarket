using System;
using System.Collections.Generic;
using System.Text;

namespace VaultAccess.Shared.Abstractions
{
    public interface IVaultAccessRepository : IConnector,IStringAccess,IAnyDataAccess
    {

    }
}
