using Kiss.Application.Interfaces.Mock;

namespace Kiss.Application.Interfaces
{

    public interface IUnitOfWork
    {
        IProductRepository Product { get; }
        IPersonRepository Person { get; }
        IIncidentReportRepository IncidentReport { get; }
        IPositionRepository Position { get; }


    }
}
