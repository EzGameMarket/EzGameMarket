using System;
using System.Collections.Generic;
using System.Runtime;
using System.Text;

namespace Shared.Utiliies.CloudStorage.Shared.Models.BaseResult
{
    public class CloudStorageUploadResult : CloudStorageBaseResult
    {
        public CloudStorageUploadResult(bool success, string fileAbsoluteUrl) : base(success)
        {
            FileAbsoluteUrl = fileAbsoluteUrl;
        }

        /// <summary>
        /// A file elérési útja, de a cloud storage domainnincs benne
        /// </summary>
        public string FileAbsoluteUrl { get; set; }
    }
}
