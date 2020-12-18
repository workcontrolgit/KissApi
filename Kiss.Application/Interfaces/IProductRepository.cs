using Kiss.Application.Parameters;
using Kiss.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kiss.Application.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<(IEnumerable<Product> Data, Pagination Pagination)> GetPagedAsync(GetAllProductsParameter urlQueryParameters);

    }
}
