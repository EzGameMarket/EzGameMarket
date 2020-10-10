using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Extensions.SettingsLoader.Services.Abstractions
{
    public interface IContainsModel<TSettingsModel>
        where TSettingsModel : class
    {
        TSettingsModel SettingsModel { get; set; }
    }
}
