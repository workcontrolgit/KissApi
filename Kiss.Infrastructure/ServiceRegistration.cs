using Microsoft.Extensions.DependencyInjection;
using Kiss.Application.Interfaces;
using Kiss.Infrastructure.Repository;
using SqlKata.Compilers;
using System;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Kiss.Application.Constants;
using SqlKata.Execution;
using Kiss.Infrastructure.Repository.Mock;
using Kiss.Application.Interfaces.Mock;

namespace Kiss.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IPersonRepository, PersonRepository>();
            services.AddTransient<IIncidentReportRepository, IncidentRepository>();
            services.AddTransient<IPositionRepository, PositionRepository>();


            services.AddTransient<IUnitOfWork, UnitOfWork>();

            //SQLKata DI Container https://sqlkata.com/docs/
            services.AddScoped(factory =>
            {
                return new QueryFactory
                {
                    Compiler = new SqlServerCompiler(),
                    Connection = new SqlConnection(configuration[ConfigurationConstants.ConnectionString]),
                    Logger = compiled => System.Diagnostics.Debug.WriteLine(compiled)
                };
            });
            //GenFu DI Container https://github.com/MisterJames/GenFu
            services.AddSingleton(typeof(IGenFuRepository<>), typeof(GenFuRepository<>));
        }
    }
}
