using GenFu;
using Kiss.Application.Interfaces;
using Kiss.Application.Interfaces.Mock;
using Kiss.Application.Parameters;
using Kiss.Core.Entities;
using SqlKata;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kiss.Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IGenFuRepository<Product> _productGeneratorService;
        private readonly QueryFactory _queryFactory;


        public ProductRepository(IGenFuRepository<Product> productGeneratorService, QueryFactory queryFactory)
        {
            _productGeneratorService = productGeneratorService;
            _queryFactory = queryFactory;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var result = await _queryFactory.Query("Products").GetAsync<Product>();
            return result;

        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            var result = await _queryFactory.Query("Products").Where("Id", "=", id)
                .FirstOrDefaultAsync<Product>();
            return result;
        }
        public async Task<(IEnumerable<Product> Data, Pagination Pagination)> GetPagedAsync(GetAllProductsParameters urlQueryParameters)
        {
            IEnumerable<Product> result;
            int recordCount =  default;


            result = await _queryFactory.Query("Products").Where("Name", urlQueryParameters.Name).ForPage(urlQueryParameters.PageNumber, urlQueryParameters.PageSize).GetAsync<Product>();


            if (urlQueryParameters.IncludeCount)
            {
                // SQLKata count https://sqlkata.com/docs/select
                var countQuery = await _queryFactory.Query("Products").Where("Name", urlQueryParameters.Name).AsCount().FirstOrDefaultAsync();
                recordCount = countQuery.count;

                //dynamic row = await _queryFactory.Query("Products").Where("Name", urlQueryParameters.Name).AsCount().FirstOrDefaultAsync();
                //int myValue = countQuery.count;
                // Dapper: How to get value from DapperRow if column name is “count(*)”?
                // https://stackoverflow.com/questions/25263701/dapper-how-to-get-value-from-dapperrow-if-column-name-is-count
                //var data = (IDictionary<string, object>)countQuery;
                //object value = data["count"];
                //recordCount = Convert.ToInt32(value);
            }

            var metadata = new Pagination
            {
                PageNumber = urlQueryParameters.PageNumber,
                PageSize = urlQueryParameters.PageSize,
                TotalRecords = recordCount

            };

            return (result, metadata);

        }

        public async Task<Guid> AddAsync(Product entity)
        {
            entity.AddedOn = DateTime.Now;
            entity.Id = await SetPrimaryKey(entity.Id);
            int affectedRows = await _queryFactory.Query("Products").InsertAsync(new
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Barcode = entity.Barcode,
                Rate = entity.Rate,
                AddedOn = entity.AddedOn
            });

            if (affectedRows != 1)
                // insert failed, return Guid structure all zero (0).  Front end should check status for empty Guid
                return Guid.Empty;

            // await SeedData();

            //return Guid of new insert row
            return entity.Id;

        }

        private async Task SeedData()
        {
            var products = await _productGeneratorService.Collection(100);

            foreach (var product in products)
            {
                await _queryFactory.Query("Products").InsertAsync(new
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Barcode = product.Barcode,
                    Rate = product.Rate,
                    AddedOn = product.AddedOn
                });
            }
        }

        public async Task<int> DeleteAsync(Guid id)
        {

            var affectedRows = await _queryFactory.Query("Products").Where("Id", "=", id).DeleteAsync();
            return affectedRows;

        }


        public async Task<int> UpdateAsync(Product entity)
        {
            entity.ModifiedOn = DateTime.Now;

            var affectedRows = await _queryFactory.Query("Products").Where("Id", entity.Id).UpdateAsync(new
            {
                Name = entity.Name,
                Description = entity.Description,
                Barcode = entity.Barcode,
                Rate = entity.Rate,
                ModifiedOn = entity.ModifiedOn,
            });

            return affectedRows;

        }

        /// <summary>
        /// Utility to setup primary key
        /// </summary>
        private async Task<Guid> SetPrimaryKey(Guid Id)
        {
            // set default key value
            var defaultKey = Guid.NewGuid();

            // check provided id is Guid format
            bool isGuid = Guid.TryParse(Id.ToString(), out Id);

            if (isGuid)
            {
                //use provided key if it has not been used.
                if (!await IsUsedPrimaryKey(Id))
                {
                    // change defaultKey to provided Id
                    defaultKey = Id;
                }
            }
            return defaultKey;
        }


        /// <summary>
        /// Check used primary key
        /// </summary>
        private async Task<bool> IsUsedPrimaryKey(Guid Id)
        {
            var result = await _queryFactory.Query("Products").Where("Id", Id)
                .FirstOrDefaultAsync<Product>();

            return result != null;
        }

    }
}
