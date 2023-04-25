using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace WebhookImplementation.ModelTest
{
    public static class ClientTest
    {

        public static string GetDataClientErrorNoFirstName()
        {
            var numUser = 30;
            var json = new
            {
                firstName = "new " + numUser,
                lasttName = "new" + numUser,
                salutation = "sr",
                email = "new" + numUser + "@gmail.com",
                mobileNumber = "5433123" + numUser
            };

            return System.Text.Json.JsonSerializer.Serialize(json);
        }


        public static string GetDataClient()
        {
            var numUser = GetRandom();
            var json = new
            {
                firstName = "new " + numUser,
                lasttName = "new" + numUser,
                salutation = "sr",
                email = "new" + numUser + "@gmail.com",
                mobileNumber = "5433123" + numUser
            };

            return System.Text.Json.JsonSerializer.Serialize(json);
        }


        private static string GetRandom()
        {
            // Obtener la fecha actual
            DateTime fechaActual = DateTime.Now;

            // Obtener el número de milisegundos transcurridos desde el inicio del día
            int milisegundos = (int)(fechaActual.TimeOfDay.TotalMilliseconds);

            // Generar un número aleatorio a partir del número de milisegundos
            Random rnd = new Random(milisegundos);
            int numeroAleatorio = rnd.Next();

            return numeroAleatorio.ToString();
        }

    }
}
