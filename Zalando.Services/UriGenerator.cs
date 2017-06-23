using System;
using System.Collections.Generic;
using Zalando.Dto;
using Zalando.Services.Contracts;
using Zalando.Services.Extensions;

namespace Zalando.Services
{
    /// <summary>
    /// Generates valid Uri given method inputs.
    /// </summary>
    public class UriGenerator : IUriGenerator
    {
        private readonly string _baseUrl;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="baseUrl">The root url of Zalando</param>
        public UriGenerator(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        /// <summary>
        /// Converts Article search parameters into a valid Uri for searching for Articles
        /// </summary>
        /// <param name="searchFacetTypes">A list of FacetTypes which will be converted into search parameters</param>
        /// <param name="currentPage">The current page number</param>
        /// <param name="pageSize">The size of each page, aka how many articles per page</param>
        /// <returns>A valid Uri that can be used to search for articles</returns>
        /// <remarks>Page size is capped at 200</remarks>
        public Uri ForArticleSearch(IEnumerable<FacetType> searchFacetTypes, uint currentPage, uint pageSize)
        {
            if (pageSize > 200)
            {
                pageSize = 200;
            }

            if (currentPage < 1)
            {
                currentPage = 1;
            }

            return new Uri($"{_baseUrl}/articles?{searchFacetTypes.ConverToUrlParameters()}&page={currentPage}&pageSize={pageSize}");
        }

        /// <summary>
        /// Creates a valid Uri for retrieving Facets
        /// </summary>
        public Uri ForFacets()
        {
            return new Uri($"{_baseUrl}/facets");
        }
    }
}
