using Kiss.Application.Interfaces;
using Kiss.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenFu;

namespace Kiss.Infrastructure.Repository
{
    public class IncidentReportRepository : IIncidentReportRepository
    {
        private readonly IBogusRepository<IncidentReport> _incidentReportGeneratorService;
        private readonly IBogusRepository<Contact> _contactGeneratorService;

        public IncidentReportRepository(IBogusRepository<IncidentReport> incidentReportGeneratorService, IBogusRepository<Contact> contactGeneratorService)
        {
            _incidentReportGeneratorService = incidentReportGeneratorService;
            _contactGeneratorService = contactGeneratorService;
        }
        public async Task<IEnumerable<IncidentReport>> GetAllAsync(int pageNumber, int pageSize)
        {
            var contacts = await _contactGeneratorService.Collection(100);

            //Example of custom data
            GenFu.GenFu.Configure<IncidentReport>()
                .Fill(p => p.ReportedBy)
                .WithRandom(contacts)
;
            IEnumerable<IncidentReport> result = await _incidentReportGeneratorService.Collection(100);

            
            return result.Skip((pageNumber - 1) * pageSize).Take(pageSize); 
        }

        public async Task<IncidentReport> GetByIdAsync(Guid id)
        {
            throw await Task.Run(() => new NotImplementedException());
        }


        public async Task<Guid> AddAsync(IncidentReport entity)
        {
            throw await Task.Run(() => new NotImplementedException());
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            throw await Task.Run(() => new NotImplementedException());
        }


        public async Task<int> UpdateAsync(IncidentReport entity)
        {
            throw await Task.Run(() => new NotImplementedException());

        }


    }
}
