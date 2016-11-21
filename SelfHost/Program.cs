using System;
using System.Net.Http;
using Api;

namespace SelfHost
{
    class Program
    {
        static void Main(string[] args)
        {
            const string baseAddress = "http://localhost:9001/";

            using (Microsoft.Owin.Hosting.WebApp.Start<Startup>(baseAddress))
            {
                var client = new HttpClient();
                var response = client.GetAsync(baseAddress + "api/user/1").Result;

                Console.WriteLine(response);
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);

                //TODO
                var response1 = client.GetAsync(baseAddress + "api/animal/user/1").Result;

                Console.WriteLine(response1);
                Console.WriteLine(response1.Content.ReadAsStringAsync().Result);

                Console.ReadLine();
            }
        }

    }
}
