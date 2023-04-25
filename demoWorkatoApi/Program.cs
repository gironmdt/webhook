using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WebhookImplementation.Helper;
using WebhookImplementation.Models;
using WebhookImplementation.ModelTest;

namespace demoWorkatoApi
{
    internal class Program
    {
       
        static string typeAuth = "API-Token";

        const string token = "63234c07ad2b1706d3018c4f26e8246ace8ff09e03533cd2704c801f444c158d";
        const string uriString = "https://apim.workato.com/aq0/workato-api-collection-v1/examplepost";
        static async Task Main(string[] args)
        {
            //await CallApi();
            var webhook = new WebhookExecute();
            WebhookDefinition webhookDefinition = new WebhookDefinition()
            {
                MethodAuthorization = typeAuth,
                MethodTry = "RetryLimit",
                Token = token,
                Uri = uriString,
                Data = ClientTest.GetDataClientErrorNoFirstName()
            };
            await webhook.Process(webhookDefinition);
            Console.WriteLine("Hello World!");
        }



        //public static async Task CallApi()
        //{
        //    client.BaseAddress = new Uri(uriString);
        //    client.DefaultRequestHeaders.Accept.Clear();
        //    client.DefaultRequestHeaders.Accept.Add(
        //        new MediaTypeWithQualityHeaderValue("application/json"));

        //    switch (typeAuth)
        //    {
        //        case "API-Token":
        //            client.DefaultRequestHeaders.Add("API-Token", token);
        //            break;
        //        case "Bearer":
        //            client.DefaultRequestHeaders.Authorization =
        //        new AuthenticationHeaderValue("Bearer", token);
        //            break;
        //        default:
        //            break;
        //    }

        //    try
        //    {
        //        var numUser = 33;
        //        var json = new
        //        {
        //            firstName= "new " + numUser,
        //            lasttName= "new" + numUser,
        //            salutation= "sr",
        //            email= "new" + numUser + "@gmail.com",
        //            mobileNumber= "5433123" + numUser
        //        };

        //        var content = new StringContent(
        //            System.Text.Json.JsonSerializer.Serialize(json),
        //            Encoding.UTF8,
        //            "application/json"
        //        );

        //        HttpResponseMessage response = await client.PostAsync(
        //            uriString,
        //            content
        //        );

        //        if (response.IsSuccessStatusCode)
        //        {
        //            string responseBody = await response.Content.ReadAsStringAsync();
        //            Console.WriteLine(responseBody);
        //        }
        //        else
        //        {
        //            Console.WriteLine("La solicitud falló con código de estado: " + response.StatusCode);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("Ocurrió un error: " + e.Message);
        //    }
        //}



    }
}
