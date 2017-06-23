using Newtonsoft.Json;
using System.Collections.Generic;

namespace Zalando.Dto
{
    public class ArticleSearchResponse
    {
        [JsonProperty("content")]
        public IEnumerable<Article> Content { get; set; }

        [JsonProperty("totalElements")]
        public int TotalElements { get; set; }

        [JsonProperty("totalPages")]
        public int TotalPages { get; set; }

        [JsonProperty("page")]
        public int Page { get; set; }

        [JsonProperty("size")]
        public int Size { get; set; }
    }
}
