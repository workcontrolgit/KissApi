using Bogus;
using Kiss.Application.Interfaces.Mock;
using Kiss.Application.Parameters;
using Kiss.Application.Parameters.Mock;
using Kiss.Core.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kiss.Infrastructure.Repository.Mock
{
    public class PositionRepository : IPositionRepository
    {
        public async Task<IEnumerable<Position>> GetAllAsync()
        {

            int mockRowCount = 100;
            Faker<Position> fakePosition;

            FakeSetup(mockRowCount, out fakePosition);

            //var fakePosition = new Faker<Position>()
            //    .RuleFor(c => c.Id, f => Guid.NewGuid())
            //    .RuleFor(c => c.FullName, f => f.Name.FullName())
            //    .RuleFor(c => c.Email, (f, u) => f.Internet.Email(u.FullName))
            //    .RuleFor(c => c.OfficePhone, f => f.Person.Phone);

            
            var result = await Task.Run(() => fakePosition.Generate(mockRowCount));

            return result; 
        }
        public async Task<(IEnumerable<Position> Data, Pagination Pagination)> GetPagedAsync(GetAllPositionsParameter urlQueryParameters)
        {
            int mockRowCount = 50000;
            int recordCount = default;

            IEnumerable<Position> result;
            Faker<Position> fakePosition;

            FakeSetup(mockRowCount, out fakePosition);

            // mock data gen
            result = await Task.Run(() => fakePosition.Generate(mockRowCount));

            // save to file
            ExportToFile(result);


            // filter
            if (!string.IsNullOrEmpty(urlQueryParameters.OrgCode))
            {
                result = result.Where(item => item.OrgCode == urlQueryParameters.OrgCode);
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

        private static void FakeSetup(int mockRowCount, out Faker<Position> fakePosition)
        {
            fakePosition = new Faker<Position>()
                .RuleFor(c => c.Id, f => Guid.NewGuid())
                .RuleFor(c => c.PositionNumber, f => f.Finance.CreditCardNumber())
                .RuleFor(c => c.Email, (f, u) => f.Internet.Email(u.FullName))
                .RuleFor(c => c.ReportsToPositionNumber, f => f.Finance.CreditCardNumber())
                .RuleFor(c => c.FullName, f => f.Name.FullName())
                .RuleFor(c => c.OfficePhone, f => f.Person.Phone)
                .RuleFor(c => c.Bureau, f => f.Commerce.Department())
                .RuleFor(c => c.OrgAbbreviation, f => f.Commerce.Department())
                .RuleFor(c => c.OrgCode, f => f.Commerce.Department())
                .RuleFor(c => c.PositionTitle, f => f.Name.JobTitle())
                .RuleFor(c => c.PositionPayPlan, f => f.Name.JobType())
                .RuleFor(c => c.PositionGrade, f => f.Name.JobArea())
                .RuleFor(c => c.PositionSeries, f => f.Name.JobArea());
        }

        private static void ExportToFile(IEnumerable<Position> result)
        {
            //serialize to json
            string json = JsonConvert.SerializeObject(result.ToArray(), Formatting.Indented);
            //write string to file
            System.IO.File.WriteAllText(@"c:\temp\jsonFile.json", json);
        }

        public async Task<Position> GetByIdAsync(Guid id)
        {
            throw await Task.Run(() => new NotImplementedException());
        }


        public async Task<Guid> AddAsync(Position entity)
        {
            throw await Task.Run(() => new NotImplementedException());
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            throw await Task.Run(() => new NotImplementedException());
        }


        public async Task<int> UpdateAsync(Position entity)
        {
            throw await Task.Run(() => new NotImplementedException());

        }


    }
}
