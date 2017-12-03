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

        private Dictionary<string, decimal> _currencies;

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
            _currencies = await GetCurrencies("dictionaries");
            var allVacancies = await GetVacancies("vacancies");

            Console.WriteLine("1. Названия профессий в вакансиях, объявленная зарплата которых превышает либо равна 120000 руб.");
            var vacancies = allVacancies.Where(v => v.Salary != null &&
                                                    IsSalaryOk(v.Salary, 120000,
                                                        (salary, wantedSalary) => salary >= wantedSalary));
            foreach (var vacancy in vacancies)
            {
                Console.WriteLine(vacancy.Name);
            }

            Console.WriteLine("2. Названия ключевых навыков в вакансиях, объявленная зарплата которых превышает либо равна 120000 руб.");
            foreach (var vacancy in vacancies)
            {
                Console.WriteLine(vacancy.Name);
                if (vacancy.KeySkills == null || !vacancy.KeySkills.Any())
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
            vacancies = allVacancies.Where(v => v.Salary != null &&
                                                IsSalaryOk(v.Salary, 15000,
                                                    (salary, wantedSalary) => salary < wantedSalary));
            foreach (var vacancy in vacancies)
            {
                Console.WriteLine(vacancy.Name);
            }

            Console.WriteLine("4. Названия ключевых навыков в вакансиях, объявленная зарплата которых менее 15000 руб.");
            foreach (var vacancy in vacancies)
            {
                Console.WriteLine(vacancy.Name);
                if (vacancy.KeySkills == null || !vacancy.KeySkills.Any())
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

        private bool IsSalaryOk(Salary salary, decimal wantedSalaryInRoubles,
            Func<decimal, decimal, bool> isSalaryOkFunc)
        {
            if (!_currencies.TryGetValue(salary.Currency, out var roublesToCurrentCurrencyCoefficient))
                return false;

            var wantedSalaryInCurrentCurrency = roublesToCurrentCurrencyCoefficient * wantedSalaryInRoubles;

            if (salary.To != null && salary.From != null)
                return isSalaryOkFunc((salary.To.Value + salary.From.Value) / 2, wantedSalaryInCurrentCurrency);

            return isSalaryOkFunc(salary.To ?? salary.From.Value, wantedSalaryInCurrentCurrency);
        }

        private async Task<IEnumerable<Vacancy>> GetVacancies(string uri)
        {
            var response = await SendRequest<GetVacanciesResponse>(uri);
            var res = new List<Vacancy>(response.Items);
            for (var i = 1; i < response.Pages; i++)
            {
                res.AddRange((await SendRequest<GetVacanciesResponse>(uri + "?per_page=" + response.PerPage + "&page=" + i)).Items);
            }
            return res;
        }

        private async Task<Dictionary<string, decimal>> GetCurrencies(string uri)
        {
            var response = await SendRequest<GetCurrenciesResponse>(uri);
            return response.Currency.ToDictionary(x => x.Code, x => x.Rate);
        }

        private async Task<T> SendRequest<T>(string uri)
        {
            var httpResponseMessage = await Client.GetAsync(uri);
            if (!httpResponseMessage.IsSuccessStatusCode)
                return default(T);

            var content = await httpResponseMessage.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}