using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using WebhookImplementation.Models;

namespace WebhookImplementation.Helper
{
    public static class WebhookAuth
    {

        public static HttpClient JwtAuth(ref HttpClient client, string token)
        {
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
            return client;
        }

        public static HttpClient ApiTokenAuth(ref HttpClient client, string token)
        {
            client.DefaultRequestHeaders.Add("API-Token", token);
            return client;
        }

    }
}
