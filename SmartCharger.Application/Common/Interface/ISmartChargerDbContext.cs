using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using DomainEntities = SmartCharger.Domain.Entities;

namespace SmartCharger.Application.Common.Interface
{
    public interface ISmartChargerDbContext
    {
        DbSet<DomainEntities.Group> Groups { get; set; }

        DbSet<DomainEntities.ChargeStation> ChargeStations { get; set; }

        DbSet<DomainEntities.Connector> Connectors { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
