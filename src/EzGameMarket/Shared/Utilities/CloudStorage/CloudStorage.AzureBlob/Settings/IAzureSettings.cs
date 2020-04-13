using Shared.Utilities.CloudStorage.Settings.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Utilities.CloudStorage.AzureBlob.Settings
{
    public interface IAzureSettings : IStorageSettings
    {
        string ConnectionString { get; set; }

        string ContainerName { get; set; }
    }
}
