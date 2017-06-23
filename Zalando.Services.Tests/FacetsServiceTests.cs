using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Zalando.Dto;
using Zalando.Services.Factories;
using Zalando.Services.Tests.Factories;

namespace Zalando.Services.Tests
{
    [TestClass]
    public class FacetsServiceUnitTests
    {

        private IHttpClientFactory _httpClientFactory;
        private FakeResponseHandler _fakeResponseHandler;
        private UriGenerator _uriGenerator;

        [TestInitialize]
        public void TestInitialize()
        {
            _fakeResponseHandler = new FakeResponseHandler();
            _httpClientFactory = new FakeHttpClientFactory(_fakeResponseHandler);

            _uriGenerator = new UriGenerator("http://localhost/api");
        }

        [TestMethod]
        public void GetFacetTypesAsync_HttpClientReturnsSuccess_ExpectGoodData()
        {
            //Arrange
            var uri = _uriGenerator.ForFacets();
            _fakeResponseHandler.AddFakeResponse(uri, new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(new List<FacetType>()))
            });
            var target = new FacetsService(_httpClientFactory, _uriGenerator);

            //Act
            var result = target.GetFacetTypesAsync().Result;

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetFacetTypesAsync_HttpClientReturnsNonSuccess_ExpectNull()
        {
            //Arrange
            var uri = _uriGenerator.ForFacets();
            _fakeResponseHandler.AddFakeResponse(uri, new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NoContent
            });
            var target = new FacetsService(_httpClientFactory, _uriGenerator);

            //Act
            var result = target.GetFacetTypesAsync().Result;

            //Assert
            Assert.IsNull(result);
        }
    }
}
