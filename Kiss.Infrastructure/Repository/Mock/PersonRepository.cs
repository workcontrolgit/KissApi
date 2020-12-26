using Kiss.Application.Interfaces;
using Kiss.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenFu;
using Kiss.Application.Interfaces.Mock;
using Kiss.Application.Parameters.Mock;
using Kiss.Application.Parameters;

namespace Kiss.Infrastructure.Repository.Mock
{
    public class PersonRepository : IPersonRepository
    {
        private readonly IGenFuRepository<Person> _personGeneratorService;
        private readonly IGenFuRepository<Contact> _contactGeneratorService;

        public PersonRepository(IGenFuRepository<Person> personGeneratorService, IGenFuRepository<Contact> contactGeneratorService)
        {
            _personGeneratorService = personGeneratorService;
            _contactGeneratorService = contactGeneratorService;
        }
        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            int mockRowCount = 100;
            IEnumerable<Person> result;
            var contacts = await _contactGeneratorService.Collection(mockRowCount);

            // custom mock data range
            GenFu.GenFu.Configure<Person>()
                .Fill(p => p.EmergencyContact)
                .WithRandom(contacts)
                .Fill(p => p.NumberOfKids)
                .WithinRange(1, 25);
            result = await _personGeneratorService.Collection(mockRowCount);
            return result; 
        }

        public async Task<(IEnumerable<Person> Data, Pagination Pagination)> GetPagedAsync(GetAllPersonsParameters urlQueryParameters)
        {
            int mockRowCount = 100;
            int recordCount = default;
            IEnumerable<Person> result;

            // mock contacts
            var contacts = await _contactGeneratorService.Collection(mockRowCount);

            // custom mock data range
            GenFu.GenFu.Configure<Person>()
                .Fill(p => p.EmergencyContact)
                .WithRandom(contacts)
                .Fill(p => p.NumberOfKids)
                .WithinRange(1, 25);
            // mock data gen
            result = await _personGeneratorService.Collection(mockRowCount);
            // filter
            if(!string.IsNullOrEmpty(urlQueryParameters.LastName))
            {
                result = result.Where(item => item.LastName == urlQueryParameters.LastName);
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


        public async Task<Person> GetByIdAsync(Guid id)
        {
            throw await Task.Run(() => new NotImplementedException());
        }


        public async Task<Guid> AddAsync(Person entity)
        {
            throw await Task.Run(() => new NotImplementedException());
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            throw await Task.Run(() => new NotImplementedException());
        }


        public async Task<int> UpdateAsync(Person entity)
        {
            throw await Task.Run(() => new NotImplementedException());

        }


    }
}
