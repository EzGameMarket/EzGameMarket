using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Utiliies.CloudStorage.Shared.Models.BaseResult
{
    public class CloudStorageBaseResult
    {
        public CloudStorageBaseResult(bool success)
        {
            Success = success;
        }

        public bool Success { get; private set; }
    }
}
