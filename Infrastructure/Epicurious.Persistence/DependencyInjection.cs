using Epicurious.Persistence.UnitofWork;
using Microsoft.Extensions.DependencyInjection;
namespace Epicurious.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
        {
            services.AddScoped<UnitOfWork>();

            return services;
        }
    }
}