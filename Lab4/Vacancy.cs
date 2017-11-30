using System.Collections.Generic;
using Newtonsoft.Json;

namespace Lab4
{
    public class Vacancy
    {
        public string Name { get; set; }

        public Salary Salary { get; set; }

        [JsonProperty("key_skills")]
        public KeySkill[] KeySkills { get; set; }
    }
}