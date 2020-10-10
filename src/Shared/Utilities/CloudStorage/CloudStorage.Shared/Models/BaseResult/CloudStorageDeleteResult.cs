using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Utiliies.CloudStorage.Shared.Models.BaseResult
{
    public class CloudStorageDeleteResult : CloudStorageBaseResult
    {
        public CloudStorageDeleteResult(bool success) : base(success)
        {
        }
    }
}
