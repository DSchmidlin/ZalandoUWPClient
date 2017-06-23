using System.Net.Http;

namespace Zalando.Services.Factories
{
    public interface IHttpClientFactory
    {
        HttpClient CreateClient();
    }
}
