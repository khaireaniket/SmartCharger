using Microsoft.EntityFrameworkCore;
using SmartCharger.Application.Common.Interface;
using SmartCharger.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace SmartCharger.Infrastructure.Persistence
{
    public class SmartChargerDbContext : DbContext, ISmartChargerDbContext
    {
        public SmartChargerDbContext(DbContextOptions<SmartChargerDbContext> options) : base(options) { }

        public DbSet<Group> Groups { get; set; }
        public DbSet<ChargeStation> ChargeStations { get; set; }
        public DbSet<Connector> Connectors { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
