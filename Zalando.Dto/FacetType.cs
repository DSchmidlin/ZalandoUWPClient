using Newtonsoft.Json;
using System.Collections.Generic;

namespace Zalando.Dto
{
    public class FacetType
    {
        [JsonProperty("filter")]
        public string Filter { get; set; }

        [JsonProperty("facets")]
        public IEnumerable<Facet> Facets { get; set; }
    }
}
