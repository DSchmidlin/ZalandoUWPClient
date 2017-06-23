using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Zalando.Dto;
using Zalando.Services.Extensions;

namespace Zalando.Services.Tests
{
    [TestClass]
    public class FacetTypesExtensionsTest
    {

        [TestMethod]
        public void ConvertTuUrlParameters_emptylist_ExpectEmptyString()
        {
            //Arrange
            var facetTypes = new List<FacetType>
            {

            };

            //Act
            var result = facetTypes.ConverToUrlParameters();

            //Assert
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void ConvertTuUrlParameters_SingleFacetTypeSingleFacet_ExpectCorrectString()
        {
            //Arrange
            var facetTypes = new List<FacetType>
            {
                new FacetType
                {
                    Filter = "abc123",
                    Facets = new List<Facet>
                    {
                        new Facet { Key = "zyx098"}
                    }
                }
            };

            //Act
            var result = facetTypes.ConverToUrlParameters();

            //Assert
            Assert.AreEqual("abc123=zyx098", result);
        }

        [TestMethod]
        public void ConvertTuUrlParameters_MultipleFacetTypeMultipleFacet_ExpectCorrectString()
        {
            //Arrange
            var facetTypes = new List<FacetType>
            {
                new FacetType
                {
                    Filter = "abc123",
                    Facets = new List<Facet>
                    {
                        new Facet { Key = "zyx098"},
                        new Facet { Key = "1234hjk"}
                    }
                },
                new FacetType
                {
                    Filter = "dog",
                    Facets = new List<Facet>
                    {
                        new Facet { Key = "water"},
                        new Facet { Key = "pen"}
                    }
                }
            };

            //Act
            var result = facetTypes.ConverToUrlParameters();

            //Assert
            Assert.AreEqual("abc123=zyx098&abc123=1234hjk&dog=water&dog=pen", result);
        }

        [TestMethod]
        public void FilterByExactName_EmptyList_ExpectEmptyList()
        {
            //Arrange
            var facetTypes = new List<FacetType>
            {

            };

            //Act
            var result = facetTypes.FilterByExactName("watch");

            //Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.Any());
        }

        [TestMethod]
        public void FilterByExactName_MultipleMatchingEntries_ExpectCorrectList()
        {
            //Arrange
            var facetTypes = new List<FacetType>
            {
                new FacetType
                {
                    Filter = "abc123",
                    Facets = new List<Facet>
                    {
                        new Facet { DisplayName = "water"},
                        new Facet { DisplayName = "1234hjk"}
                    }
                },
                new FacetType
                {
                    Filter = "dog",
                    Facets = new List<Facet>
                    {
                        new Facet { DisplayName = "water"},
                        new Facet { DisplayName = "pen"}
                    }
                }
            };

            //Act
            var result = facetTypes.FilterByExactName("wAter");

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(2, result.SelectMany(x => x.Facets).Count());
        }

        [TestMethod]
        public void FilterByPartialName_EmptyList_ExpectEmptyList()
        {
            //Arrange
            var facetTypes = new List<FacetType>
            {

            };

            //Act
            var result = facetTypes.FilterByPartialName("watch");

            //Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.Any());
        }

        [TestMethod]
        public void FilterByPartialName_MultipleMatchingEntries_ExpectCorrectList()
        {
            //Arrange
            var facetTypes = new List<FacetType>
            {
                new FacetType
                {
                    Filter = "abc123",
                    Facets = new List<Facet>
                    {
                        new Facet { DisplayName = "waterIsCold"},
                        new Facet { DisplayName = "1234hjk"}
                    }
                },
                new FacetType
                {
                    Filter = "dog",
                    Facets = new List<Facet>
                    {
                        new Facet { DisplayName = "coffeeishot"},
                        new Facet { DisplayName = "pen"}
                    }
                }
            };

            //Act
            var result = facetTypes.FilterByPartialName("IS");

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(2, result.SelectMany(x => x.Facets).Count());
        }
    }
}
