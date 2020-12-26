using Bogus;
using Kiss.Application.Interfaces.Mock;
using Kiss.Application.Parameters;
using Kiss.Application.Parameters.Mock;
using Kiss.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kiss.Infrastructure.Repository.Mock
{
    public class IncidentRepository : IIncidentReportRepository
    {
        public async Task<IEnumerable<Incident>> GetAllAsync()
        {
            Faker<Incident> fakeIncidentReport = FakeData();

            var result = await Task.Run(() => fakeIncidentReport.Generate(10));

            return result;
        }

        private static Faker<Incident> FakeData()
        {
            Randomizer.Seed = new Random(1338);
            var types = new[] { "Type 1", "Type 2", "Type 3", "Type 4", "Type 5" };
            var fakeContact = new Faker<Contact>()
                .RuleFor(c => c.Id, f => Guid.NewGuid())
                .RuleFor(c => c.FirstName, f => f.Name.FirstName())
                .RuleFor(c => c.LastName, f => f.Name.LastName())
                .RuleFor(c => c.EmailAddress, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
                .RuleFor(c => c.PhoneNumber, f => f.Person.Phone);


            var fakeIncidentReport = new Faker<Incident>()
                .RuleFor(o => o.Id, f => Guid.NewGuid())
                .RuleFor(o => o.Description, f => f.Lorem.Paragraph(3))
                .RuleFor(o => o.Type, f => f.PickRandom(types))
                .RuleFor(o => o.ReportedOn, f => f.Date.Past())
                .RuleFor(o => o.ReportedBy, f => fakeContact.Generate());
            
            return fakeIncidentReport;
        }

        public async Task<(IEnumerable<Incident> Data, Pagination Pagination)> GetPagedAsync(GetAllIncidentsParameters urlQueryParameters)
        {
            int recordCount = default;
            Faker<Incident> fakeIncidentReport = FakeData();

            IEnumerable<Incident> result;

            result = await Task.Run(() => fakeIncidentReport.Generate(100).ToList());

            // filter
            if (!(urlQueryParameters.Type == default))
            {
                //result = result.Where(o => DbFunctions.TruncateTime(o.ReportedOn).Equals(urlQueryParameters.ReportedOn));
                result = result.Where(o => o.Type.Equals(urlQueryParameters.Type));
            }
            // update recordCount before page
            if (urlQueryParameters.IncludeCount)
            {
                recordCount = result.Count();
            }
            // page
            result = result.Skip(urlQueryParameters.PageNumber).Take(urlQueryParameters.PageSize);


            var metadata = new Pagination
            {
                PageNumber = urlQueryParameters.PageNumber,
                PageSize = urlQueryParameters.PageSize,
                TotalRecords = recordCount

            };

            return (result, metadata);

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
