using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Utilities.CloudStorage.AzureBlob.Settings
{
    public class AzureSettings : IAzureSettings
    {
        public string ConnectionString { get; set; }
        public string ContainerName { get; set; }

        public string GetFilesContainerName() => ContainerName;

        public string GetServerAccess() => ConnectionString;

        public void SetFilesContainerName(string value) => ContainerName = value;

        public void SetServerAccess(string value) => ConnectionString = value;
    }
}
