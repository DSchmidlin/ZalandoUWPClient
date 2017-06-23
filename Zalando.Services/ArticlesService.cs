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
    /// Client Service for working with Articles.
    /// See : https://github.com/zalando/shop-api-documentation/wiki/Articles
    /// </summary>
    public class ArticlesService : IArticlesService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IUriGenerator _uriGenerator;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="httpClientFactory">A factory for creating Http Clients; used to make REST API calls</param>
        /// <param name="uriGenerator">A class to generate valid URIs</param>
        public ArticlesService(IHttpClientFactory httpClientFactory, IUriGenerator uriGenerator)
        {
            _httpClientFactory = httpClientFactory;
            _uriGenerator = uriGenerator;
        }

        /// <summary>
        /// Searches for articles filtered based on the users FaceetTypes.
        /// </summary>
        /// <param name="searchFacetTypes">A list of FacetTypes which will be converted into search parameters</param>
        /// <param name="currentPage">The current page number</param>
        /// <param name="pageSize">The size of each page, aka how many articles per page</param>
        /// <returns>An ArticleSearchReponse object that contains Article and paging information.</returns>
        /// <remarks>It may be necessary to call this method multiple times to retrieve all the Articles as the Service
        /// has paging turned on.  PageSize is capped at 200</remarks>
        public async Task<ArticleSearchResponse> SearchAsync(IEnumerable<FacetType> searchFacetTypes, uint currentPage, uint pageSize)
        {
            var searchUri = _uriGenerator.ForArticleSearch(searchFacetTypes, currentPage, pageSize);

            var client = _httpClientFactory.CreateClient();
            var result = await client.GetAsync(searchUri);

            if (result.StatusCode != HttpStatusCode.OK)
            {
                return null;
            }

            var stringContent = await result.Content.ReadAsStringAsync();
            var searchResponse = JsonConvert.DeserializeObject<ArticleSearchResponse>(stringContent);

            return searchResponse;
        }
    }
}
