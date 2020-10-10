using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Shared.Utiliies.CloudStorage.Shared.Models.BaseResult
{
    public class CloudStorageDownloadResult : CloudStorageBaseResult
    {
        public CloudStorageDownloadResult(bool success, Stream file) : base(success)
        {
            File = file;
        }

        public Stream File { get; set; }
    }
}
