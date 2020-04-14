using Shared.Utilities.Billing.Settings.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace Shared.Utilities.Billing.Settings.Services.Implementations
{
    class BillingSettingsLoader<TSettingsModel> : IBillingSettingsLoader<TSettingsModel>
        where TSettingsModel : class
    {
        public TSettingsModel Load(string filePath)
        {
            if (File.Exists(filePath) == false)
            {
                throw new FileNotFoundException($"A {filePath} fájl nem található");
            }

            var text = File.ReadAllText(filePath);

            return JsonConvert.DeserializeObject<TSettingsModel>(text);
        }

        public async Task<TSettingsModel> LoadAsync(string filePath)
        {
            if (File.Exists(filePath) == false)
            {
                throw new FileNotFoundException($"A {filePath} fájl nem található");
            }

            var text = await File.ReadAllTextAsync(filePath);

            return JsonConvert.DeserializeObject<TSettingsModel>(text);
        }
    }
}
