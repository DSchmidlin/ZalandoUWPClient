using Newtonsoft.Json;

namespace Zalando.Dto
{
    public class Unit
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("size")]
        public string Size { get; set; }

        [JsonProperty("price")]
        public Price Price { get; set; }

        [JsonProperty("originalPrice")]
        public Price OriginalPrice { get; set; }

        [JsonProperty("available")]
        public bool Available { get; set; }

        [JsonProperty("stock")]
        public int Stock { get; set; }
    }
}
