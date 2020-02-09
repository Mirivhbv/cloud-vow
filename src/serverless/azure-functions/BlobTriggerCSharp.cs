using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Serverless.Lambdas
{
    public static class BlobTriggerCSharp
    {
        [FunctionName("BlobTriggerCSharp")]
        public static void Run(
            // uploaded:       images/isuuer.jpg
            // I will upload:  images/isuuer.min.jpg
            [BlobTrigger("images/{name}.{ext}",
            Connection = "blob_connection")]
            Stream myBlob,
            [Blob("imagesmin/{name}.{ext}",
            FileAccess.Write,
            Connection="blob_connection")]
            Stream minified,
            string name, string ext,
            ILogger log
            )
        {
            if (ext.Contains("min"))
                return;

           
        }
    }
}
