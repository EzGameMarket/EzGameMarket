using System;
using System.Collections.Generic;
using System.Text;

namespace VaultAccess.Shared
{
    public interface IAnyDataAccess
    {
        T GetByteData<T>(string key);
        void SetByteData<T>(string key, T data);
    }
}
