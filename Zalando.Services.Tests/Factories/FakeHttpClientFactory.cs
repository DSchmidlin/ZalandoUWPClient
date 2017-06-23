using System.Net.Http;
using Zalando.Services.Factories;

namespace Zalando.Services.Tests.Factories
{
    public class FakeHttpClientFactory : IHttpClientFactory
    {
        private DelegatingHandler _responseHandler;

        public FakeHttpClientFactory(DelegatingHandler responseHandler)
        {
            _responseHandler = responseHandler;
        }

        public HttpClient CreateClient()
        {
            return new HttpClient(_responseHandler);
        }
    }

}
