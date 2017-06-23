using MoreLinq;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Zalando.Dto
{
    public class Article
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("modelId")]
        public string ModelId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("shopUrl")]
        public string ShopUrl { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("available")]
        public bool Available { get; set; }

        [JsonProperty("season")]
        public string Season { get; set; }

        [JsonProperty("seasonYear")]
        public string SeasonYear { get; set; }

        [JsonProperty("activationDate")]
        public string ActivationDate { get; set; }

        [JsonProperty("genders")]
        public IEnumerable<string> Genders { get; set; }

        [JsonProperty("ageGroups")]
        public IEnumerable<string> AgeGroups { get; set; }

        [JsonProperty("brand")]
        public Brand Brand { get; set; }

        [JsonProperty("categoryKeys")]
        public IEnumerable<string> CategoryKeys { get; set; }

        [JsonProperty("attributes")]
        public IEnumerable<Attribute> Attributes { get; set; }

        [JsonProperty("units")]
        public IEnumerable<Unit> Units { get; set; }

        [JsonProperty("media")]
        public Media Media { get; set; }

        [JsonIgnore]
        public string OriginalPriceFormatted
        {
            get
            {
                return Units.MinBy(x => x.OriginalPrice.Value)
                    .OriginalPrice.Formatted;
            }
        }

        [JsonIgnore]
        public string CurrentPriceFormatted
        {
            get
            {
                return Units.MinBy(x => x.Price.Value)
                    .Price.Formatted;
            }
        }

    }
}
