using System.Threading.Tasks;

namespace Shared.Extensions.SettingsLoader.Services.Abstractions
{
    public interface ILoadSettingsService<TSettings> where TSettings : class
    {
        TSettings Load();
        Task<TSettings> LoadAsync();
    }
}