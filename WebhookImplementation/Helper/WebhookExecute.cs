using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebhookImplementation.Models;
using System.Linq.Expressions;
using WebhookImplementation.ModelTest;

namespace WebhookImplementation.Helper
{
    public class WebhookExecute
    {
        

       

        public delegate Task<bool> WebhookTry(Func<WebhookDefinition, Task<ApiResponse<string>>> sendWebhook, WebhookDefinition webhookDefinition);


        private static Dictionary<string, WebhookTry> actionsTry = new Dictionary<string, WebhookTry>()
        {
            { "ExponentialBackoff", Retry.SendWebhookWithExponentialBackoffAsync },
            { "LinearBackoff", Retry.SendWebhookWithLinearBackoffAsync },
            { "RetryLimit", Retry.SendWebhookWithRetryLimitAsync },
            { "FixedInterval", Retry.SendWebhookWithFixedIntervalAsync }

        };

        





        public async Task Process(WebhookDefinition webhookDefinition)
        {

            // Ejecutar una acción del diccionario
            var webhookCallApi = new WebhookCallApi();
            if (actionsTry.TryGetValue(webhookDefinition.MethodTry, out WebhookTry handler))
            {
                Func<WebhookDefinition, Task<ApiResponse<string>>> sendWebhook = webhookCallApi.CallApi;
                await handler(sendWebhook, webhookDefinition);
            }
            
        }

        private void LogMessage(string message)
        {
            Console.WriteLine(message);
        }

    }
}
