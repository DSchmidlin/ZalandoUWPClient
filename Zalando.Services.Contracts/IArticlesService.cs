using System.Collections.Generic;
using System.Threading.Tasks;
using Zalando.Dto;

namespace Zalando.Services.Contracts
{
    public interface IArticlesService
    {
        Task<ArticleSearchResponse> SearchAsync(IEnumerable<FacetType> searchFacetTypes, uint currentPage, uint pageSize);
    }
}
