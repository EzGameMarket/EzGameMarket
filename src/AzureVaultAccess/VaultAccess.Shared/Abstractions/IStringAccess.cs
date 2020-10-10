namespace VaultAccess.Shared.Abstractions
{
    public interface IStringAccess
    {
        string GetString(string key);

        void SetString(string key, string data);
    }
}