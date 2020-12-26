using Kiss.Application.Parameters;
using Kiss.Application.Parameters.Mock;
using Kiss.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kiss.Application.Interfaces.Mock
{
    public interface IPositionRepository : IGenericRepository<Position>
    {
        Task<(IEnumerable<Position> Data, Pagination Pagination)> GetPagedAsync(GetAllPositionsParameters urlQueryParameters);

    }
}
