using Newtonsoft.Json;

namespace Zalando.Dto
{
    public class Facet
    {
        public Facet()
        {
            Key = string.Empty;
            DisplayName = string.Empty;
            Count = 0;
        }

        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }
    }
}
