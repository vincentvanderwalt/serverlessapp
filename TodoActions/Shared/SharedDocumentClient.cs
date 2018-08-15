using Microsoft.Azure.Documents.Client;
using System;
using Microsoft.Extensions.Configuration;


namespace TodoActions.Shared
{
    public static class SharedDocumentClient
    {
        public static DocumentClient GetCustomClient()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            DocumentClient customClient = new DocumentClient(
                new Uri(config["CosmosDBAccountEndpoint"]),
                config["CosmosDBAccountKey"],
                new ConnectionPolicy
                {
                    ConnectionMode = ConnectionMode.Direct,
                    ConnectionProtocol = Protocol.Tcp,
                    // Customize retry options for Throttled requests
                    RetryOptions = new RetryOptions()
                    {
                        MaxRetryAttemptsOnThrottledRequests = 10,
                        MaxRetryWaitTimeInSeconds = 30
                    }
                });

            return customClient;
        }
    }
}
