using System;
using System.Collections.Generic;
using System.Text;

namespace VaultAccess.Shared
{
    public interface IStringAccess
    {
        string GetString(string key);
        void SetString(string key, string data);
    }
}
