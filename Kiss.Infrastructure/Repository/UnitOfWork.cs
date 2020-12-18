using Kiss.Application.Interfaces;
using Kiss.Application.Interfaces.Mock;

namespace Kiss.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IProductRepository productRepository, 
            IPersonRepository personRepository, 
            IIncidentReportRepository incidentReportRepository,
            IPositionRepository positionRepository)
        {
            Product = productRepository;
            Person = personRepository;
            IncidentReport = incidentReportRepository;
            Position = positionRepository;

        }
        public IProductRepository Product { get; }
        public IPersonRepository Person { get; }
        public IIncidentReportRepository IncidentReport { get; }
        public IPositionRepository Position { get; }


    }
}
