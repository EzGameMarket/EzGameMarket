using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Utilities.CloudStorage.Settings.Services.Abstractions
{
    public interface IStorageSettings
    {
        string GetServerAccess();
        void SetServerAccess(string value);
        string GetFilesContainerName();
        void SetFilesContainerName(string value);
    }
}
