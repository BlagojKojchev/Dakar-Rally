using DakarRally.Logic.Behaviours;
using DakarRally.Logic.CommonValidation;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DakarRally.Logic
{
    public static class DependencyInjection
    {
        public static void AddDomainDependencies(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddScoped<IVehicleValidation, VehicleValidation>();
            services.AddScoped<IRaceValidation, RaceValidation>();
            services.AddScoped<IVehicleTypeValidation, VehicleTypeValidation>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
