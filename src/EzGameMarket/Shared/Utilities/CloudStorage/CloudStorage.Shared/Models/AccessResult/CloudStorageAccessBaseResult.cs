using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Utiliies.CloudStorage.Shared.Models.AccessResult
{
    public class CloudStorageAccessBaseResult
    {
        public CloudStorageAccessBaseResult(bool success)
        {
            Success = success;
        }

        public bool Success { get; private set; }
    }
}
