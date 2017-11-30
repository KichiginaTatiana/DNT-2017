using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            Client = new HttpClient()
            {
                BaseAddress = new Uri("https://api.hh.ru")
            };
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Client.DefaultRequestHeaders.Add("User-Agent", "api-test-agent");

            Task.Run(async () => await GetVacancies("vacancies")).Wait();
        }

        private static HttpClient Client { get; set; }

        private static async Task<IEnumerable<Vacancy>> GetVacancies(string uri)
        {
            var response = await SendRequest(uri);
            var res = new List<Vacancy>(response.Items);
            for (var i = 1; i < response.Pages; i++)
            {
                res.AddRange((await SendRequest(uri + "?per_page=" + response.PerPage + "&page=" + i)).Items);
            }
            return res;
        }

        private static async Task<Response> SendRequest(string uri)
        {
            var httpResponseMessage = await Client.GetAsync(uri);
            if (!httpResponseMessage.IsSuccessStatusCode)
                return null;

            var content = await httpResponseMessage.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Response>(content);
        }
    }
}
