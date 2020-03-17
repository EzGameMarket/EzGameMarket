using System;
using System.Collections.Generic;
using System.Text;

namespace VaultAccess.Shared
{
    public interface IVaultAccessRepository : IConnector,IStringAccess,IAnyDataAccess
    {

    }
}
