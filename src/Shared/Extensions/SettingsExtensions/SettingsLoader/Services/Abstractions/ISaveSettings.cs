using System.Threading.Tasks;

namespace Shared.Extensions.SettingsLoader.Services.Abstractions
{
    public interface ISaveSettings<TSettings> where TSettings : class
    {
        bool Save(TSettings data, string filePath);
        Task<bool> SaveAsync(TSettings data, string filePath);
    }
}