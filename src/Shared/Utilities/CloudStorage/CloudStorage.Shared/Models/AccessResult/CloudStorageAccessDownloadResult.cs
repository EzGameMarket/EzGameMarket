using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Shared.Utiliies.CloudStorage.Shared.Models.AccessResult
{
    public class CloudStorageAccessDownloadResult : CloudStorageAccessBaseResult
    {
        public CloudStorageAccessDownloadResult(bool success, Stream file) : base(success)
        {
            File = file;
        }

        public Stream File { get; set; }
    }
}
