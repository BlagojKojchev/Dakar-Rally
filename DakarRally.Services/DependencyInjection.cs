using DakarRally.Services.RaceServices;
using DakarRally.Services.VehicleServices;
using Microsoft.Extensions.DependencyInjection;

namespace DakarRally.Services
{
    public static class DependencyInjection
    {
        public static void AddServiceDependencies(this IServiceCollection services)
        {
            services.AddScoped<IVehicleService, VehicleService>();
            services.AddScoped<IRaceStatisticsService, RaceStatisticsService>();
            services.AddScoped<IRaceService, RaceService>();
        }
    }
}
