
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TodoActions.Shared;

namespace TodoActions
{
    public static class CustomRead
    {
        private static readonly DocumentClient client = SharedDocumentClient.GetCustomClient();

        [FunctionName("CustomRead")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]HttpRequest req, ILogger log, ExecutionContext context)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(context.FunctionAppDirectory)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            string requestBody = new StreamReader(req.Body).ReadToEnd();            

            Uri collectionUri = UriFactory.CreateDocumentCollectionUri(config["CosmosDBName"], config["CosmosDBCollectionName"]);

            SqlQuery data = JsonConvert.DeserializeObject<SqlQuery>(requestBody);

            FeedOptions queryOptions = new FeedOptions { MaxItemCount = -1 };

            try
            {
                List<TodoModel> todoData = client.CreateDocumentQuery<TodoModel>(
                collectionUri, data.QueryText, queryOptions).ToList();

                return new OkObjectResult(JsonConvert.SerializeObject(todoData));
            }
            catch (Exception error)
            {

                return new BadRequestObjectResult($"Error trying to read documents from cosmosdb {error.Message}");
            }            

        }
    }
}
