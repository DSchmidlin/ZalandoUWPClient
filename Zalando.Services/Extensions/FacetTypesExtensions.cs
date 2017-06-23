using System.Collections.Generic;
using System.Linq;
using Zalando.Dto;

namespace Zalando.Services.Extensions
{
    /// <summary>
    /// Extensions methods for the FacetTypes class
    /// </summary>
    public static class FacetTypesExtensions
    {
        /// <summary>
        /// Converts an enumeration of FacetTypes into a partial Url string that can be used in filters
        /// </summary>
        /// <param name="facetTypes">The FacetTypes to convert.</param>
        /// <returns>A string containing Filters to Keys that can be used in a Url such as Articles.</returns>
        public static string ConverToUrlParameters(this IEnumerable<FacetType> facetTypes)
        {
            var result = facetTypes.Select(x => string.Join("&", x.Facets.Select(y => $"{x.Filter}={y.Key}")));
            return string.Join("&",result);
        }

        /// <summary>
        /// Filters an enumberation of FacetTypes by a Facet DisplayName.  The name must match exactly but is not case insensitive.
        /// </summary>
        /// <param name="facetTypes">The FacetTypes to search</param>
        /// <param name="facetName">The Facet display name to filter on.</param>
        /// <returns>A reduced set of FacetTypes and each facet type having a reduced set of Facets.</returns>
        public static IEnumerable<FacetType> FilterByExactName(this IEnumerable<FacetType> facetTypes, string facetName)
        {
            return facetTypes.Select(x => new FacetType { Filter = x.Filter, Facets = x.Facets.Where(y => y.DisplayName
                                                                                                            .ToLower()
                                                                                                            .Equals(facetName.ToLower())) })
                             .Where(x => x.Facets.Any());
        }

        /// <summary>
        /// Filters an enumberation of FacetTypes by a Facet DisplayName.  The name is a partial match but is not case insensitive.
        /// </summary>
        /// <param name="facetTypes">The FacetTypes to search</param>
        /// <param name="facetName">The Facet display name to filter on.</param>
        /// <returns>A reduced set of FacetTypes and each facet type having a reduced set of Facets.</returns>
        public static IEnumerable<FacetType> FilterByPartialName(this IEnumerable<FacetType> facetTypes, string facetName)
        {
            return facetTypes.Select(x => new FacetType { Filter = x.Filter, Facets = x.Facets.Where(y => y.DisplayName
                                                                                                                .ToLower()
                                                                                                                .Contains(facetName.ToLower())) })
                             .Where(x => x.Facets.Any());
        }
    }
}
