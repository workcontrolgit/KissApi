using Kiss.Application.Parameters;
using Kiss.Application.Parameters.Mock;
using Kiss.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kiss.Application.Interfaces.Mock
{
    public interface IPersonRepository : IGenericRepository<Person>
    {
        Task<(IEnumerable<Person> Data, Pagination Pagination)> GetPagedAsync(GetAllPersonsParameters urlQueryParameters);

    }
}
