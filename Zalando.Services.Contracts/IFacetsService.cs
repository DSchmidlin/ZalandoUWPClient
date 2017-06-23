using System.Collections.Generic;
using System.Threading.Tasks;
using Zalando.Dto;

namespace Zalando.Services.Contracts
{
    public interface IFacetsService
    {
        Task<IEnumerable<FacetType>> GetFacetTypesAsync();
    }
}
