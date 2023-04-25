using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json.Linq;
using WebhookImplementation.Helper;
using WebhookImplementation.Models;
using WebhookImplementation.ModelTest;

namespace WebhookImplementation.Queue
{
    public class QueueAzure
    {
        static string typeAuth = "API-Token";
        const string token = "63234c07ad2b1706d3018c4f26e8246ace8ff09e03533cd2704c801f444c158d";
        const string uriString = "https://apim.workato.com/aq0/workato-api-collection-v1/examplepost";
        public async Task Start(WebhookDefinition webhookDefinition)
        {
            // Retrieve the connection string for the storage account.
            string connectionString = "DefaultEndpointsProtocol=https;AccountName=<account-name>;AccountKey=<account-key>;EndpointSuffix=core.windows.net";

            // Create a queue client object.
            CloudQueueClient queueClient = CloudStorageAccount.Parse(connectionString).CreateCloudQueueClient();

            // Retrieve a reference to a queue.
            CloudQueue queue = queueClient.GetQueueReference("contacts");

            // Create the queue if it doesn't already exist.
            await queue.CreateIfNotExistsAsync();

            var content = Newtonsoft.Json.JsonConvert.SerializeObject(webhookDefinition);

            // Add a message to the queue.
            CloudQueueMessage message = new CloudQueueMessage(content);
            await queue.AddMessageAsync(message);

            // Process messages from the queue.
            while (true)
            {
                CloudQueueMessage receivedMessage = await queue.GetMessageAsync();

                if (receivedMessage != null)
                {
                    try
                    {
                        // Process the message and add the contact to the third-party API.
                        await CallExternal(receivedMessage.AsString);

                        // Delete the message from the queue.
                        await queue.DeleteMessageAsync(receivedMessage);
                    }
                    catch (Exception ex)
                    {
                        // Handle the error and move the message to the back of the queue.
                        Console.WriteLine($"Error adding contact to third-party API: {ex.Message}");
                        await queue.UpdateMessageAsync(receivedMessage, TimeSpan.FromSeconds(30), MessageUpdateFields.Visibility);
                    }
                }

                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }

        public   async Task CallExternal(string webhookDefinitionString)
        {
            var webhook = new WebhookExecute();
            WebhookDefinition webhookDefinition = Newtonsoft.Json.JsonConvert.DeserializeObject<WebhookDefinition>(webhookDefinitionString);

            await webhook.Process(webhookDefinition);
        }
    }
}
