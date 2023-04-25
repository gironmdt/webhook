using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using static WebhookImplementation.Helper.WebhookExecute;
using System.Threading.Tasks;
using WebhookImplementation.Models;
using WebhookImplementation.ModelTest;
using WebhookImplementation.Exceptions;
using System.Net;

namespace WebhookImplementation.Helper
{
    public class WebhookCallApi
    {

        public delegate HttpClient MethodAuthentication(ref HttpClient client, string token);

        private static Dictionary<string, MethodAuthentication> actionsAuth = new Dictionary<string, MethodAuthentication>()
        {
            { "API-Token", WebhookAuth.ApiTokenAuth },
            { "Bearer", WebhookAuth.JwtAuth }
        };
        public async Task<ApiResponse<string>> CallApi(WebhookDefinition webhookDefinition)
        {
            var client = new HttpClient();
            var uri = new Uri(webhookDefinition.Uri);

            if (uri.Scheme != Uri.UriSchemeHttps)
            {
                throw new SecurityException("URL must use HTTPS protocol", 1001);
            }

            client.BaseAddress = uri;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            //Add headers to authtorization
            if (actionsAuth.TryGetValue(webhookDefinition.MethodAuthorization, out MethodAuthentication handler))
                handler(ref client, webhookDefinition.Token);

            try
            {
                var content = new StringContent(
                    webhookDefinition.Data,
                    Encoding.UTF8,
                    "application/json"
                );

                HttpResponseMessage response = await client.PostAsync(
                    uri,
                    content
                );

                string responseBody = await response.Content.ReadAsStringAsync();
                return response.StatusCode switch
                {
                    HttpStatusCode.OK => new ApiResponse<string>(responseBody),
                    HttpStatusCode.NotFound => throw new ClientApiException($"The resource was not found: {response.ReasonPhrase} {response.StatusCode}", 1001) ,
                    _ when (int)response.StatusCode >= 400 && (int)response.StatusCode < 500 => throw new ClientApiException($"Client error: {response.ReasonPhrase} { response.StatusCode}", 1002),
                    _ when (int)response.StatusCode >= 500 && (int)response.StatusCode < 600 => throw new ClientApiException($"Server error: {response.ReasonPhrase} {response.StatusCode}", 1003) ,
                    _ => throw new ClientApiException($"Unknown error: {response.ReasonPhrase} {response.StatusCode}", 1004)
                };
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
