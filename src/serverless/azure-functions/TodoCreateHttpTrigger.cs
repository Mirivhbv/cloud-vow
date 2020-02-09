using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Serverless.Lambdas
{
    public static class TodoCreateHttpTrigger
    {

        [FunctionName("TodoCreateHttpTrigger")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "todos")] HttpRequest req,
            [CosmosDB("tracks", "todos", ConnectionStringSetting = "connstr")] IAsyncCollector<dynamic> todos,
            ILogger log)
        {
            dynamic data = null;
            using (var r = new StreamReader(req.Body))
            {
                var json = await r.ReadToEndAsync();
                data = JsonConvert.DeserializeObject(json);
                await todos.AddAsync(data);
            }

            return new OkObjectResult(data);
        }
    }
}