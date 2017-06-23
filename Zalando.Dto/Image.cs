using Newtonsoft.Json;

namespace Zalando.Dto
{
    public class Image
    {
        [JsonProperty("orderNumber")]
        public int OrderNumber { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("thumbnailHdUrl")]
        public string ThumbNailHDUrl { get; set; }

        [JsonProperty("smallUrl")]
        public string SmallUrl { get; set; }

        [JsonProperty("smallHdUrl")]
        public string SmallHDUrl { get; set; }

        [JsonProperty("mediumUrl")]
        public string MediumUrl { get; set; }

        [JsonProperty("mediumHdUrl")]
        public string MediumHdUrl { get; set; }

        [JsonProperty("largeUrl")]
        public string LargeUrl { get; set; }

        [JsonProperty("largeHdUrl")]
        public string LargeHdUrl { get; set; }
    }
}
