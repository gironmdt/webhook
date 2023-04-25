using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebhookImplementation.Models;

namespace WebhookImplementation.Helper
{
    public static class Retry
    {

        const int MAX_RETRY_COUNT = 2;
        
        private static async Task<bool> ManageWebhook(Func<WebhookDefinition, Task<ApiResponse<string>> > sendWebhook, WebhookDefinition webhookDefinition)
        {
           var response = await sendWebhook(webhookDefinition);
           return response.Success;
        }

        public static async Task<bool> SendWebhookWithExponentialBackoffAsync(Func<WebhookDefinition, Task<ApiResponse<string>>> sendWebhook, WebhookDefinition webhookDefinition)
        {
            int retryCount = 0;
            int delay = 1;
            bool success = false;

            while (!success && retryCount < MAX_RETRY_COUNT)
            {
                try
                {
                    // Code to send webhook
                    success= await ManageWebhook(sendWebhook, webhookDefinition);
                }
                catch (Exception ex)
                {
                    // Handle error
                    retryCount++;
                    await Task.Delay(delay * 1000);
                    delay *= 2;
                }
            }

            return success;
        }

        public static async Task<bool> SendWebhookWithLinearBackoffAsync(Func<WebhookDefinition, Task<ApiResponse<string>>> sendWebhook, WebhookDefinition webhookDefinition)
        {
            int retryCount = 0;
            int delay = 1;
            bool success = false;

            while (!success && retryCount < MAX_RETRY_COUNT)
            {
                try
                {
                    success = await ManageWebhook(sendWebhook, webhookDefinition);
                }
                catch (Exception ex)
                {
                    // Handle error
                    retryCount++;
                    await Task.Delay(delay * 1000);
                    delay += 1;
                }
            }

            return success;
        }

        public static async Task<bool> SendWebhookWithFixedIntervalAsync(Func<WebhookDefinition, Task<ApiResponse<string>>> sendWebhook, WebhookDefinition webhookDefinition)
        {
            int retryCount = 0;
            int delay = 5;
            bool success = false;

            while (!success && retryCount < MAX_RETRY_COUNT)
            {
                try
                {
                    success = await ManageWebhook(sendWebhook, webhookDefinition);
                }
                catch (Exception ex)
                {
                    // Handle error
                    retryCount++;
                    await Task.Delay(delay * 1000);
                }
            }

            return success;
        }

        public static async Task<bool> SendWebhookWithRetryLimitAsync(Func<WebhookDefinition, Task<ApiResponse<string>>> sendWebhook, WebhookDefinition webhookDefinition)
        {
            int retryCount = 0;
            bool success = false;

            while (!success && retryCount < MAX_RETRY_COUNT)
            {
                try
                {
                    success = await ManageWebhook(sendWebhook, webhookDefinition);
                }
                catch (Exception ex)
                {
                    // Handle error
                    retryCount++;
                }
            }

            if (!success)
            {
                // Inform user of the problem and ask for manual action
            }

            return success;
        }


    }
}
