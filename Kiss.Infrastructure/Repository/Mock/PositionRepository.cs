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

            int mockRowCount = 40000;
            //int mockRowCount = 400;
            Faker<Position> fakePosition;

            FakeSetup(mockRowCount, out fakePosition);
           
            var result = await Task.Run(() => fakePosition.Generate(mockRowCount));

            // save to file
            CustomExportToFile(result);



            return result; 
        }
        public async Task<(IEnumerable<Position> Data, Pagination Pagination)> GetPagedAsync(GetAllPositionsParameters urlQueryParameters)
        {
            int mockRowCount = 40000;
            //int mockRowCount = 400;
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

        public static void FakeSetup(int mockRowCount, out Faker<Position> fakePosition)
        {
            Randomizer.Seed = new Random(1338);
            var PositionTypes = new[] {"D", "S", "X"};
            var PayPayPlans = new[] {"GS", "GM", "GL", "GP", "EX"};
            var PayGrades = new[] {"05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15"};
            var Series = new[] { "0000", "1000", "2000", "3000", "4000", "5000", "6000", "7000", "8000", "9000"};
            fakePosition = new Faker<Position>()
                .RuleFor(c => c.PositionNumber, f => f.PickRandom(PositionTypes) + f.Random.Long(1000000, 9999999).ToString())
                .RuleFor(c => c.FullName, f => f.Name.FullName())
                .RuleFor(c => c.Email, (f, u) => f.Internet.Email(u.FullName))
                .RuleFor(c => c.ReportsToPositionNumber, f => f.PickRandom(PositionTypes) + f.Random.Long(1000000, 9999999).ToString())
                .RuleFor(c => c.OfficePhone, f => f.Person.Phone)
                .RuleFor(c => c.Bureau, f => f.Commerce.Department())
                .RuleFor(c => c.OrgAbbreviation, f => f.Commerce.Department())
                .RuleFor(c => c.OrgCode, f => f.Random.Long(100000, 999999).ToString())
                .RuleFor(c => c.PositionTitle, f => f.Name.JobTitle())
                .RuleFor(c => c.PositionPayPlan, f => f.PickRandom(PayPayPlans))
                .RuleFor(c => c.PositionGrade, f => f.PickRandom(PayGrades))
                .RuleFor(c => c.PositionSeries, f => f.PickRandom(Series));
        }

        private static void ExportToFile(IEnumerable<Position> result)
        {
            //serialize to json
            string json = JsonConvert.SerializeObject(result.ToArray(), Formatting.Indented);
            //write string to file
            System.IO.File.WriteAllText(@"c:\temp\OrgPositionExport.json", json);
        }

        private static void CustomExportToFile(IEnumerable<Position> result)
        {
            List<PositionExport> customResult = new List<PositionExport>();
            foreach (var position in result)
            {
                var obj = new PositionExport
                {
                    PositionNumber = new PositionNumber
                    {
                        value = position.PositionNumber,
                        name = "Position Number"
                    },
                    Email = new Email
                    {
                        value = position.Email,
                        name = "Email"
                    },
                    ReportsToPositionNumber = new ReportsToPositionNumber
                    {
                        value = position.ReportsToPositionNumber,
                        name = "Reports To Position Number"
                    },
                    FullName = new FullName
                    {
                        value = position.FullName,
                        name = "Full Name"
                    },
                    OfficePhone = new OfficePhone
                    {
                        value = position.OfficePhone,
                        name = "Office Phone"
                    },
                    Bureau = new Bureau
                    {
                        value = position.Bureau,
                        name = "Bureau"
                    },
                    OrgAbbreviation = new OrgAbbreviation
                    {
                        value = position.OrgAbbreviation,
                        name = "Org Abbreviation"
                    },
                    OrgCode = new OrgCode
                    {
                        value = position.OrgCode,
                        name = "Org Code"
                    },
                    PositionTitle = new PositionTitle
                    {
                        value = position.PositionTitle,
                        name = "Position Title"
                    },
                    PositionPayPlan = new PositionPayPlan
                    {
                        value = position.PositionPayPlan,
                        name = "Position Pay Plan"
                    },
                    PositionGrade = new PositionGrade
                    {
                        value = position.PositionGrade,
                        name = "Position Grade"
                    },
                    PositionSeries = new PositionSeries
                    {
                        value = position.PositionSeries,
                        name = "Position Series"
                    },
                };
                customResult.Add(obj);
            }
            //serialize to json
            string json = JsonConvert.SerializeObject(customResult.ToArray(), Formatting.Indented);
            //write string to file
            System.IO.File.WriteAllText(@"c:\temp\OrgPositionCustomExport.json", json);
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
