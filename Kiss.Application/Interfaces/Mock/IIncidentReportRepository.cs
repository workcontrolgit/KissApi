﻿using Kiss.Application.Parameters;
using Kiss.Application.Parameters.Mock;
using Kiss.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kiss.Application.Interfaces.Mock
{
    public interface IIncidentReportRepository : IGenericRepository<Incident>
    {
        Task<(IEnumerable<Incident> Data, Pagination Pagination)> GetPagedAsync(GetAllIncidentsParameters urlQueryParameters);

    }
}
