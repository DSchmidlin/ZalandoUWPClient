using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Zalando.Dto;
using Zalando.Services.Contracts;
using Zalando.Services.Factories;

namespace Zalando.Services
{
    /// <summary>
    /// Client Service for working with Facets
    /// See: https://github.com/zalando/shop-api-documentation/wiki/Facets
    /// </summary>
    public class FacetsService : IFacetsService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IUriGenerator _uriGenerator;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="httpClientFactory">A factory for creating Http Clients; used to make REST API calls</param>
        /// <param name="uriGenerator">A class to generate valid URIs</param>
        public FacetsService(IHttpClientFactory httpClientFactory, IUriGenerator uriGenerator)
        {
            _httpClientFactory = httpClientFactory;
            _uriGenerator = uriGenerator;
        }

        /// <summary>
        /// Retrieves all the Facets from Zalando, which can be used later when searching
        /// for articles
        /// </summary>
        /// <returns>An enumeration of FacetTypes</returns>
        public async Task<IEnumerable<FacetType>> GetFacetTypesAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var result = await client.GetAsync(_uriGenerator.ForFacets());

            if (result.StatusCode != HttpStatusCode.OK)
            {
                return null;
            }

            var stringContent = await result.Content.ReadAsStringAsync();
            var facetTypes = JsonConvert.DeserializeObject<IEnumerable<FacetType>>(stringContent);

            return facetTypes;
        }
    }
}
