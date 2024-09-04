using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceBus.Application.Services;
using ServiceBus.Interfaces.Services;

namespace ServiceBus.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IServiceBusService, ServiceBusService>();
            return services;
        }
    }
}
