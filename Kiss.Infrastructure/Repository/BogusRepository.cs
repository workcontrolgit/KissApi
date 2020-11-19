using Kiss.Application.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bogus;
using Bogus.DataSets;
using Bogus.Extensions;

namespace Kiss.Infrastructure.Repository
{
    class BogusRepository<T> : IBogusRepository<T>
        where T : class, new()
    {
        /// <summary>
        /// Generates a collection of type T based on the properties in T
        /// </summary>
        /// <returns>List<T></returns>
        public async Task<IEnumerable<T>> Collection()
        {
            return await Task.Run(() => (IEnumerable<T>) new Faker<T>().Generate());
        }

        /// <summary>
        /// Generates the collection of type T of size = length 
        /// </summary>
        /// <param name="length">The size of the collection to be passed</param>
        /// <returns>A collection of type T based on the length passed</returns>
        public async Task<IEnumerable<T>> Collection(int length)
        {
            return await Task.Run(() => (IEnumerable<T>) new Faker<T>().Generate(length));
        }

        /// <summary>
        /// Generates an object of type T with data
        /// </summary>
        /// <returns>T with data based on the properties in T</returns>
        public async Task<T> Instance()
        {
            return await Task.Run(() => new Faker<T>());
        }
    }

}
