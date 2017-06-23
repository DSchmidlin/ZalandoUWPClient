using Newtonsoft.Json;
using System.Collections.Generic;

namespace Zalando.Dto
{
    public class Attribute
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("values")]
        public IEnumerable<string> Values { get; set; }
    }
}
