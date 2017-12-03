using System.Collections.Generic;
using Newtonsoft.Json;

namespace Lab4
{
    public class Vacancy
    {
        public string Name { get; set; }

        public Salary Salary { get; set; }

        [JsonProperty("key_skills")]
        public IEnumerable<KeySkill> KeySkills { get; set; }
    }
}