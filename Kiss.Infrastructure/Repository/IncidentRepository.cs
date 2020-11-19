using Kiss.Application.Interfaces;
using Kiss.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenFu;
using Bogus;

namespace Kiss.Infrastructure.Repository
{
    public class IncidentRepository : IIncidentReportRepository
    {
        //private readonly IBogusRepository<IncidentReport> _incidentReportGeneratorService;
        //private readonly IBogusRepository<Contact> _contactGeneratorService;

        //public IncidentReportRepository(IBogusRepository<IncidentReport> incidentReportGeneratorService, IBogusRepository<Contact> contactGeneratorService)
        //{
        //    _incidentReportGeneratorService = incidentReportGeneratorService;
        //    _contactGeneratorService = contactGeneratorService;
        //}
        public async Task<IEnumerable<Incident>> GetAllAsync(int pageNumber, int pageSize)
        {

            var fakeContact = new Faker<Contact>()
                .RuleFor(c => c.Id, f => Guid.NewGuid())
                .RuleFor(c => c.FirstName, f => f.Name.FirstName())
                .RuleFor(c => c.LastName, f => f.Name.LastName())
                .RuleFor(c => c.EmailAddress, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
                .RuleFor(c => c.PhoneNumber, f => f.Person.Phone);


            var fakeIncidentReport = new Faker<Incident>()
                .RuleFor(o => o.Id, f => Guid.NewGuid())
                .RuleFor(o => o.Description, f => f.Lorem.Word())
                .RuleFor(o => o.ReportedOn, f => f.Date.Past())
                .RuleFor(o => o.ReportedBy, f => fakeContact.Generate());
            
            var result = await Task.Run(() => fakeIncidentReport.Generate(100));

            return result.Skip((pageNumber - 1) * pageSize).Take(pageSize); 
        }

        public async Task<Incident> GetByIdAsync(Guid id)
        {
            throw await Task.Run(() => new NotImplementedException());
        }


        public async Task<Guid> AddAsync(Incident entity)
        {
            throw await Task.Run(() => new NotImplementedException());
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            throw await Task.Run(() => new NotImplementedException());
        }


        public async Task<int> UpdateAsync(Incident entity)
        {
            throw await Task.Run(() => new NotImplementedException());

        }


    }
}
