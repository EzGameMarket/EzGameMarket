using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace VaultAccess.Shared.Infrastucture
{
    public class ByteConverter<T> : IByteConverter<T> where T : class
    {
        public T Deserialze(byte[] data)
        {
            using (var memoryStream = new MemoryStream(data))
            {
                return (new BinaryFormatter()).Deserialize(memoryStream) as T;
            }
        }

        public byte[] Serialize(T data)
        {
            using (var memoryStream = new MemoryStream())
            {
                (new BinaryFormatter()).Serialize(memoryStream, data);
                return memoryStream.ToArray();
            }
        }
    }
}
