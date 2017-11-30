using System.Collections.Generic;
using Newtonsoft.Json;

namespace Lab4
{
    public class Response
    {
        public IEnumerable<Vacancy> Items { get; set; }

        public int Pages { get; set; }

        [JsonProperty("per_page")]
        public int PerPage { get; set; }
    }
}