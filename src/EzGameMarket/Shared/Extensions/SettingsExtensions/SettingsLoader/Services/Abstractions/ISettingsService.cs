using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Extensions.SettingsLoader.Services.Abstractions
{
    public interface ISettingsService<TSettings> : 
        ISaveSettingsService<TSettings>, 
        ILoadSettingsService<TSettings>
            where TSettings : class
    {
        string FilePath { get; }
    }
}
