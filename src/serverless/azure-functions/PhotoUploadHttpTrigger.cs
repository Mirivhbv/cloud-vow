using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Serverless.Lambdas
{
    public static class PhotoUploadHttpTrigger
    {
        [FunctionName("PhotoUploadHttpTrigger")]
        public static async Task<IActionResult> Run(
            [HttpTrigger (AuthorizationLevel.Anonymous,
                "post", Route = "photos")]
            HttpRequest req,
            Binder binder)
        {

            var file = req.Form.Files.FirstOrDefault();
            var path = $"images/{file.FileName}";
            var attrs = new Attribute[] {
                new BlobAttribute(path, FileAccess.Write),
                new StorageAccountAttribute("blob_connection")
            };
            var stream = await binder.BindAsync<Stream>(attrs);
            await file.CopyToAsync(stream);
            stream.Dispose();
            
            return new OkObjectResult(file.FileName);
        }
    }
}