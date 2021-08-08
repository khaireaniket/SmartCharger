using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using DomainEntities = SmartCharger.Domain.Entities;

namespace SmartCharger.Infrastructure.Persistence
{
    public class SmartChargerDataGenerator
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new SmartChargerDbContext(serviceProvider.GetRequiredService<DbContextOptions<SmartChargerDbContext>>()))
            {
                if (!context.Groups.Any())
                {
                    context.Groups.AddRange(
                        new DomainEntities.Group
                        {
                            Id = new Random().Next(),
                            Name = "Group 1",
                            CapacityInAmps = 100F
                        },
                         new DomainEntities.Group
                         {
                             Id = new Random().Next(),
                             Name = "Group 2",
                             CapacityInAmps = 80F
                         }
                    );

                    await context.SaveChangesAsync();
                }

                if (!context.ChargeStations.Any())
                {
                    context.ChargeStations.AddRange(
                        new DomainEntities.ChargeStation
                        {
                            Id = new Random().Next(),
                            Name = "Charge Station 1 for Group 1",
                            GroupId = context.Groups.Skip(0).Take(1).First().Id
                        },
                         new DomainEntities.ChargeStation
                         {
                             Id = new Random().Next(),
                             Name = "Charge Station 2 for Group 1",
                             GroupId = context.Groups.Skip(0).Take(1).First().Id
                         },
                         new DomainEntities.ChargeStation
                         {
                             Id = new Random().Next(),
                             Name = "Charge Station 1 for Group 2",
                             GroupId = context.Groups.Skip(1).Take(1).First().Id
                         },
                         new DomainEntities.ChargeStation
                         {
                             Id = new Random().Next(),
                             Name = "Charge Station 2 for Group 2",
                             GroupId = context.Groups.Skip(1).Take(1).First().Id
                         }
                    );

                    await context.SaveChangesAsync();
                }

                if (!context.Connectors.Any())
                {
                    context.Connectors.AddRange(
                        new DomainEntities.Connector
                        {
                            Id = 1,
                            MaxCurrentInAmps = 10F,
                            ChargeStationId = context.ChargeStations.Skip(0).Take(1).First().Id
                        },
                        new DomainEntities.Connector
                        {
                            Id = 2,
                            MaxCurrentInAmps = 15F,
                            ChargeStationId = context.ChargeStations.Skip(0).Take(1).First().Id
                        },
                        new DomainEntities.Connector
                        {
                            Id = 3,
                            MaxCurrentInAmps = 20F,
                            ChargeStationId = context.ChargeStations.Skip(0).Take(1).First().Id
                        },
                        new DomainEntities.Connector
                        {
                            Id = 4,
                            MaxCurrentInAmps = 25F,
                            ChargeStationId = context.ChargeStations.Skip(1).Take(1).First().Id
                        },
                        new DomainEntities.Connector
                        {
                            Id = 5,
                            MaxCurrentInAmps = 30F,
                            ChargeStationId = context.ChargeStations.Skip(1).Take(1).First().Id
                        }
                    );

                    await context.SaveChangesAsync();
                }

                await context.Groups.ForEachAsync(async group =>
                {
                    if (group.ChargeStations.Count <= 0)
                    {
                        group.ChargeStations = await context.ChargeStations.Where(chargeStation => chargeStation.GroupId == group.Id).ToListAsync();
                    }

                    await context.SaveChangesAsync();
                });

                await context.ChargeStations.ForEachAsync(async chargeStation =>
                {
                    if (chargeStation.Connectors.Count <= 0)
                    {
                        chargeStation.Connectors = await context.Connectors.Where(connector => connector.ChargeStationId == chargeStation.Id).ToListAsync();
                    }

                    await context.SaveChangesAsync();
                });
            }
        }
    }
}
