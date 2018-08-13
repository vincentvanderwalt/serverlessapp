
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using Microsoft.Azure.Documents.Client;
using System;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace GetTodosFunction
{
    public static class GetTodos
    {




        //private static readonly string endpointUrl = ConfigurationManager.AppSettings["cosmosDBAccountEndpoint"];
        //private static readonly string authorizationKey = ConfigurationManager.AppSettings["cosmosDBAccountKey"];
        //private static readonly DocumentClient client = new DocumentClient(new Uri(endpointUrl), authorizationKey);

        [FunctionName("GetTodos")]
        public static async Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Function, "get","options", Route = null)]HttpRequest req,
            [CosmosDB("ToDoList", "Items", ConnectionStringSetting = "CosmosDBConnection")] DocumentClient client,
            TraceWriter log, ExecutionContext context)
        {
            
            Uri collectionUri = UriFactory.CreateDocumentCollectionUri("ToDoList", "Items");

            var docs = await client.ReadDocumentFeedAsync(collectionUri, new FeedOptions { MaxItemCount = 10 });


            return docs != null
                ? (ActionResult)new OkObjectResult(docs)
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }
    }
}
