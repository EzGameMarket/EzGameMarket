using System.Threading.Tasks;

namespace Shared.Extensions.SettingsLoader.Services.Abstractions
{
    public interface ISaveSettingsService<TSettings> where TSettings : class
    {
        bool Save(TSettings data);
        Task<bool> SaveAsync(TSettings data);
    }
}