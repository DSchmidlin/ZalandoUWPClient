using System;
using System.Collections.Generic;
using Zalando.Dto;

namespace Zalando.Services.Contracts
{
    public interface IUriGenerator
    {
        Uri ForFacets();

        Uri ForArticleSearch( IEnumerable<FacetType> searchFacetTypes, uint currentPage, uint pageSize);


    }
}
