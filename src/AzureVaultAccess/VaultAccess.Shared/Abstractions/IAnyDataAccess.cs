namespace VaultAccess.Shared.Abstractions
{
    public interface IAnyDataAccess
    {
        T GetByteData<T>(string key);

        void SetByteData<T>(string key, T data);
    }
}