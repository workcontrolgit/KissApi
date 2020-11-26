using Kiss.Application.Interfaces;
using Kiss.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenFu;
using Kiss.Application.Interfaces.Mock;

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
        public async Task<IEnumerable<Person>> GetAllAsync(int pageNumber, int pageSize)
        {
            var contacts = await _contactGeneratorService.Collection(100);

            //Example of custom data
            GenFu.GenFu.Configure<Person>()
                .Fill(p => p.EmergencyContact)
                .WithRandom(contacts)
                .Fill(p => p.NumberOfKids)
                .WithinRange(1, 25);
            IEnumerable<Person> result = await _personGeneratorService.Collection(100);
            return result.Skip((pageNumber - 1) * pageSize).Take(pageSize); 
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
