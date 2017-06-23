using Newtonsoft.Json;

namespace Zalando.Dto
{
    public class Price
    {
        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("value")]
        public double Value { get; set; }

        [JsonProperty("formatted")]
        public string Formatted { get; set; }
    }
}
