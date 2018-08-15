
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Documents.Client;
using System;
using System.Threading.Tasks;

namespace TodoActions
{
    public static class GetAll
    {
        [FunctionName("GetAll")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]HttpRequest req,
            [CosmosDB("ToDoList", "Items", ConnectionStringSetting = "CosmosDBConnection")] DocumentClient client,
            ILogger log)
        {
            Uri collectionUri = UriFactory.CreateDocumentCollectionUri("ToDoList", "Items");

            var docs = await client.ReadDocumentFeedAsync(collectionUri, new FeedOptions { MaxItemCount = 10 });

            return docs != null
                ? (ActionResult)new OkObjectResult(docs)
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }
    }
}
