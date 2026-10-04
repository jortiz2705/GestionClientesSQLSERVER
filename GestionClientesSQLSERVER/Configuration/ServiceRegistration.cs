using GestionClientesSQLSERVER.Repositories;
using GestionClientesSQLSERVER.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GestionClientesSQLSERVER.Configuration
{
    public static class ServiceRegistration
    {
        public static IServiceCollection RegistrarDependencias(
        this IServiceCollection services)
        {
            services.AddScoped<IClienteRepository, ClienteRepositorySp>();

            services.AddScoped<IClienteService, ClienteService>();

            return services;
        }
    }
}
