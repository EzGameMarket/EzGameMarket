namespace VaultAccess.Shared.Infrastucture
{
    public interface IByteConverter<T> where T : class
    {
        T Deserialze(byte[] data);

        byte[] Serialize(T data);
    }
}