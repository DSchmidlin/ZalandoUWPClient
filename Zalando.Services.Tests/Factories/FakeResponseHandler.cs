using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Zalando.Services.Tests.Factories
{
    /// <summary>
    /// Dave S: This was found here: http://stackoverflow.com/questions/22223223/how-to-pass-in-a-mocked-httpclient-in-a-net-test
    /// </summary>    
    public class FakeResponseHandler : DelegatingHandler
    {
        private readonly Dictionary<Uri, HttpResponseMessage> _fakeResponses = new Dictionary<Uri, HttpResponseMessage>();
        private readonly Dictionary<Uri, Dictionary<string, HttpResponseMessage>> _fakeResponsesWithContentFilter = new Dictionary<Uri, Dictionary<string, HttpResponseMessage>>();

        public void AddFakeResponse(Uri uri, HttpResponseMessage responseMessage)
        {
            _fakeResponses[uri] = responseMessage;
        }

        public void AddFakeResponse(Uri uri, HttpResponseMessage responseMessage, HttpContent requestContent)
        {
            if (requestContent == null)
            {
                AddFakeResponse(uri, responseMessage);
                return;
            }

            if (!_fakeResponsesWithContentFilter.ContainsKey(uri))
            {
                _fakeResponsesWithContentFilter[uri] = new Dictionary<string, HttpResponseMessage>();
            }

            var contentString = requestContent.ReadAsStringAsync().Result;

            _fakeResponsesWithContentFilter[uri][contentString] = responseMessage;
        }

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            var contentString = request.Content?.ReadAsStringAsync().Result;

            if (_fakeResponsesWithContentFilter.ContainsKey(request.RequestUri) &&
                _fakeResponsesWithContentFilter[request.RequestUri].ContainsKey(contentString))
            {
                return _fakeResponsesWithContentFilter[request.RequestUri][contentString];
            }

            if (_fakeResponses.ContainsKey(request.RequestUri))
            {
                return _fakeResponses[request.RequestUri];
            }

            return new HttpResponseMessage(HttpStatusCode.NotFound) { RequestMessage = request };
        }
    }


}
