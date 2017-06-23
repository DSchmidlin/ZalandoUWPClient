using Newtonsoft.Json;

namespace Zalando.Dto
{
    public class BrandFamily
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("shopUrl")]
        public string ShopUrl { get; set; }
    }
}
