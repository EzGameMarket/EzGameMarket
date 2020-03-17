using System;
using System.Collections.Generic;
using System.Text;

namespace VaultAccess.Shared
{
    public interface IByteConverter<T> where T : class
    {
        T Deserialze(byte[] data);
        byte[] Serialize(T data);
    }
}
