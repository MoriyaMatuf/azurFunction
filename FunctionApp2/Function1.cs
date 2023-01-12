using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text.Json;
namespace FunctionApp2
{
    class car{
        public int price { get; set; }
        public string name { get; set; }
    }
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            car c = new car() { name = "orit", price = 12 };
            string name = req.Query["name"];
            string lastname = req.Query["lastname"];
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;
            string responsMessage = System.Text.Json.JsonSerializer.Serialize(c);
            return new OkObjectResult(responsMessage);
            //return name != null
            //    ? (ActionResult)new OkObjectResult($"Hello,{lastname} {name}")
            //    : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }
    }
}
