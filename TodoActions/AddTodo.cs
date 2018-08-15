
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Documents.Client;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System;
using TodoActions.Shared;

namespace TodoActions
{

    

    public static class AddTodo
    {
        private static readonly DocumentClient client = SharedDocumentClient.GetCustomClient();

        [FunctionName("AddTodo")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]HttpRequest req, ILogger log, ExecutionContext context)
        {

            var config = new ConfigurationBuilder()
                .SetBasePath(context.FunctionAppDirectory)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            string requestBody = new StreamReader(req.Body).ReadToEnd();
            TodoModel data = JsonConvert.DeserializeObject<TodoModel>(requestBody);

            Uri collectionUri = UriFactory.CreateDocumentCollectionUri(config["CosmosDBName"], config["CosmosDBCollectionName"]);

            var response = await client.CreateDocumentAsync(collectionUri, data);

            if (!string.IsNullOrWhiteSpace(response.Resource.Id))
            {
                return new OkObjectResult(response.Resource);
            }
            else
            {
                return new BadRequestObjectResult("Error adding record to CosmosDb");
            }
        }
    }
}
