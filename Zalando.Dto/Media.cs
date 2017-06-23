using Newtonsoft.Json;
using System.Collections.Generic;

namespace Zalando.Dto
{
    public class Media
    {
        [JsonProperty("images")]
        public IEnumerable<Image> Images { get; set; }
    }
}
