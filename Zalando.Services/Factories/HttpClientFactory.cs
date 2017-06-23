using System.Net.Http;

namespace Zalando.Services.Factories
{
    /// <summary>
    /// A factory that creates HttpClients
    /// </summary>
    public class HttpClientFactory : IHttpClientFactory
    {
        /// <summary>
        /// Generate an HttpClient that can be used to make REST API calls
        /// </summary>
        public HttpClient CreateClient()
        {
            var client = new HttpClient();

            return client;
        }
    }
}
