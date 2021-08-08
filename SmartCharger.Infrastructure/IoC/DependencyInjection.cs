using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SmartCharger.Application.Common.Interface;
using SmartCharger.Infrastructure.Persistence;

namespace SmartCharger.Infrastructure.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
        {
            services.AddDbContext<SmartChargerDbContext>(options => options.UseInMemoryDatabase(databaseName: "SmartCharger"), ServiceLifetime.Singleton);
            services.AddSingleton<ISmartChargerDbContext>(provider => provider.GetService<SmartChargerDbContext>());
            return services;
        }
    }
}
