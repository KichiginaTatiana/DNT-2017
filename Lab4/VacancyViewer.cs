using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Lab4
{
    public class VacancyViewer
    {
        private HttpClient Client { get; }

        public VacancyViewer()
        {
            Client = new HttpClient()
            {
                BaseAddress = new Uri("https://api.hh.ru")
            };
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Client.DefaultRequestHeaders.Add("User-Agent", "api-test-agent");
        }

        public async Task View()
        {
            var allVacancies = await GetVacancies("vacancies");

            Console.WriteLine("1. Названия профессий в вакансиях, объявленная зарплата которых превышает либо равна 120000 руб.");
            var vacancies = allVacancies.Where(v => v.Salary != null
                                                    && (v.Salary.To != null && v.Salary.From != null &&
                                                        (v.Salary.To + v.Salary.From) / 2 >= 120000
                                                        || v.Salary.To != null && v.Salary.To >= 120000
                                                        || v.Salary.From != null && v.Salary.From >= 120000));
            foreach (var vacancy in vacancies)
            {
                Console.WriteLine(vacancy.Name);
            }

            Console.WriteLine("2. Названия ключевых навыков в вакансиях, объявленная зарплата которых превышает либо равна 120000 руб.");
            foreach (var vacancy in vacancies)
            {
                Console.WriteLine(vacancy.Name);
                if (vacancy.KeySkills == null || vacancy.KeySkills.Length == 0)
                {
                    Console.WriteLine("Нет ключевых навыков");
                    continue;
                }

                foreach (var skill in vacancy.KeySkills)
                {
                    Console.WriteLine(skill);
                }
            }

            Console.WriteLine("3. Названия профессий в вакансиях, объявленная зарплата которых менее 15000 руб.");
            vacancies = allVacancies.Where(v => v.Salary != null
                                                    && (v.Salary.To != null && v.Salary.From != null &&
                                                        (v.Salary.To + v.Salary.From) / 2 < 15000
                                                        || v.Salary.To != null && v.Salary.To < 15000
                                                        || v.Salary.From != null && v.Salary.From < 15000));
            foreach (var vacancy in vacancies)
            {
                Console.WriteLine(vacancy.Name);
            }

            Console.WriteLine("4. Названия ключивых навыков в вакансиях, объявленная зарплата которых менее 15000 руб.");
            foreach (var vacancy in vacancies)
            {
                Console.WriteLine(vacancy.Name);
                if (vacancy.KeySkills == null || vacancy.KeySkills.Length == 0)
                {
                    Console.WriteLine("Нет ключевых навыков");
                    continue;
                }

                foreach (var skill in vacancy.KeySkills)
                {
                    Console.WriteLine(skill);
                }
            }
        }

        private async Task<IEnumerable<Vacancy>> GetVacancies(string uri)
        {
            var response = await SendRequest(uri);
            var res = new List<Vacancy>(response.Items);
            for (var i = 1; i < response.Pages; i++)
            {
                res.AddRange((await SendRequest(uri + "?per_page=" + response.PerPage + "&page=" + i)).Items);
            }
            return res;
        }

        private async Task<Response> SendRequest(string uri)
        {
            var httpResponseMessage = await Client.GetAsync(uri);
            if (!httpResponseMessage.IsSuccessStatusCode)
                return null;

            var content = await httpResponseMessage.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Response>(content);
        }
    }
}