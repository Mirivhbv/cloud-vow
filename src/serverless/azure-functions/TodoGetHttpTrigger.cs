using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Serverless.Lambdas
{
    public static class TodoGetHttpTrigger
    {
        [FunctionName("TodoGetHttpTrigger")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "todos")] HttpRequest req,
            [CosmosDB("tracks", "todos", ConnectionStringSetting = "connstr", SqlQuery = "SELECT * FROM c")] IEnumerable<dynamic> todos,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            return new OkObjectResult(todos);
        }
    }
}