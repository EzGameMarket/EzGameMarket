using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Shared.Utiliies.CloudStorage.Shared.Models.AccessResult
{
    public class CloudStorageAccessUploadResult : CloudStorageAccessBaseResult
    {
        public CloudStorageAccessUploadResult(bool success, string fileURL) : base(success)
        {
            AbsoluteFileURL = fileURL;
        }

        public string AbsoluteFileURL { get; private set; }
    }
}
