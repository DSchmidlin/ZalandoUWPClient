using Newtonsoft.Json;

namespace Zalando.Dto
{
    public class Brand
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("logoUrl")]
        public string LogoUrl { get; set; }

        [JsonProperty("shopUrl")]
        public string ShopUrl { get; set; }

        [JsonProperty("brandFamily")]
        public BrandFamily BrandFamily {get; set; }
    }
}
