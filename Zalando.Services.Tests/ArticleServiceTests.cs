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
    public class ArticleServiceTests
    {
        private IHttpClientFactory _httpClientFactory;
        private FakeResponseHandler _fakeResponseHandler;
        private UriGenerator _uriGenerator;

        [TestInitialize]
        public void TestInitialize()
        {
            _fakeResponseHandler = new FakeResponseHandler();
            _httpClientFactory = new FakeHttpClientFactory(_fakeResponseHandler);

            _uriGenerator = new UriGenerator("http://localhost/articles");
        }

        [TestMethod]
        public void ArticleSearch_HttpClientReturnsSuccess_ExpectGoodData()
        {
            //Arrange
            var searchFacetTypes = new List<FacetType>();
            uint currentPage = 1;
            uint pageSize = 5;

            var searchUri = _uriGenerator.ForArticleSearch(searchFacetTypes, currentPage, pageSize);

            _fakeResponseHandler.AddFakeResponse(searchUri, new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(new ArticleSearchResponse()))
            });

            var target = new ArticlesService(_httpClientFactory, _uriGenerator);

            //Act
            var result = target.SearchAsync(searchFacetTypes, currentPage, pageSize).Result;

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ArticleSearch_HttpClientReturnsNonSuccess_ExpectNull()
        {
            //Arrange
            var searchFacetTypes = new List<FacetType>();
            uint currentPage = 1;
            uint pageSize = 5;

            var searchUri = _uriGenerator.ForArticleSearch(searchFacetTypes, currentPage, pageSize);

            _fakeResponseHandler.AddFakeResponse(searchUri, new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NoContent,
            });

            var target = new ArticlesService(_httpClientFactory, _uriGenerator);

            //Act
            var result = target.SearchAsync(searchFacetTypes, currentPage, pageSize).Result;

            //Assert
            Assert.IsNull(result);
        }
    }
}
