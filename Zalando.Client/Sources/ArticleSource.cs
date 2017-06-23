using Microsoft.Toolkit.Uwp;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Zalando.Dto;
using Zalando.Services.Contracts;

namespace Zalando.Client.Sources
{
    public class ArticleSource : IIncrementalSource<Article>
    {
        private readonly IArticlesService _articleService;
        private readonly IEnumerable<FacetType> _searchFacetTypes;

        public ArticleSource(IArticlesService articlService, IEnumerable<FacetType> searchFacetTypes)
        {
            _articleService = articlService;
            _searchFacetTypes = searchFacetTypes;
        }

        public async Task<IEnumerable<Article>> GetPagedItemsAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default(CancellationToken))
        {
            var results = await _articleService.SearchAsync(_searchFacetTypes, (uint)pageIndex, (uint)pageSize);
            return results.Content;
        }
    }
}
