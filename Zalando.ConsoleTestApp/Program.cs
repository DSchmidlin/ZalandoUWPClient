using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zalando.Dto;
using Zalando.Services;
using Zalando.Services.Factories;

namespace Zalando.ConsoleTestApp
{
    class Program
    {
        static HttpClientFactory _httpClientFactory;
        static UriGenerator _uriGenerator;

        static void Main(string[] args)
        {

            Log.Logger = new LoggerConfiguration()
                .WriteTo.ColoredConsole()
                .CreateLogger();

            _httpClientFactory = new HttpClientFactory();
            _uriGenerator = new UriGenerator("https://api.zalando.com");

            GetFacetsAndLog().Wait();
            SearchForArticlesAndLog().Wait();

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }

        static async Task SearchForArticlesAndLog()
        {
            var articleService = new ArticlesService(_httpClientFactory, _uriGenerator);

            var facetTypes = new List<FacetType>
            {
                new FacetType
                {
                    Filter = "brandFamily",
                    Facets = new List<Facet> { new Facet { Key = "nike" } }
                }
            };

            var searchResult = await articleService.SearchAsync(facetTypes, 1, pageSize: 5);
        }

        static async Task GetFacetsAndLog()
        {
            var facetsService = new FacetsService(_httpClientFactory, _uriGenerator);

            var facetTypes = await facetsService.GetFacetTypesAsync();

            LogFacetTypes(facetTypes);
        }

        static void LogFacetTypes(IEnumerable<FacetType> facetTypes)
        {
            foreach(var facetType in facetTypes)
            {
                Log.Information("Filter : {filter}", facetType.Filter);
                LogFacet(facetType.Facets);
            }
        }

        static void LogFacet(IEnumerable<Facet> facets)
        {
            foreach(var facet in facets)
            {
                Log.Information("Key : {key}, DisplayName : {displayName}, Count : {count}",facet.Key, facet.DisplayName, facet.Count);
            }
        }
    }
}