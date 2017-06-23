using Zalando.Client.Containers;
using Zalando.Services;
using Zalando.Services.Contracts;
using Zalando.Services.Factories;

namespace Zalando.Client.Extensions
{
    public static class IContainerExtensions
    {
        public static void RegisterDefaults(this IContainer container)
        {
            container.Register<IUriGenerator, UriGenerator>(new UriGenerator("https://api.zalando.com"));
            container.Register<IHttpClientFactory, HttpClientFactory>();
            container.Register<IFacetsService, FacetsService>();
            container.Register<IArticlesService, ArticlesService>();
        }
    }
}
