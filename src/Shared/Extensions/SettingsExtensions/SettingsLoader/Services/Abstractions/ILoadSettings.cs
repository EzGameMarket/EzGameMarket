using System.Threading.Tasks;

namespace Shared.Extensions.SettingsLoader.Services.Abstractions
{
    public interface ILoadSettings<TSettings> where TSettings : class
    {
        TSettings Load(string filePath);
        Task<TSettings> LoadAsync(string filePath);
    }
}