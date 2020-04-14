using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Utilities.Billing.Settings.Services.Abstractions
{
    public interface IBillingSettingsLoader<TSettingsModel> where TSettingsModel : class
    {
        TSettingsModel Load(string filePath);
        Task<TSettingsModel> LoadAsync(string filePath);
    }
}
