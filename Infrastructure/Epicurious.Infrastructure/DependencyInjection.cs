using Epicurious.Domain.Identity;
using Epicurious.Infrastructure.Contexts.Application;
using Epicurious.Infrastructure.Contexts.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Epicurious.Infrastructure
{
    public static class DependencyInjection
    {
        //private static readonly IConfiguration _configuration;

        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionStringApplication = configuration.GetSection("EpicuriousApplicationSQLDB").Value;
            var connectionStringIdentity = configuration.GetSection("EpicuriousIdentitySQLDB").Value;

            services.AddDbContext<EpicuriousIdentityContext>(options=>
            {
                options.UseNpgsql(connectionStringIdentity);
            });

            //services.AddDbContext<ApplicationDbContext>(options =>
            //{
            //    options.UseNpgsql(connectionStringApplication);
            //});

            //services.AddDbContextFactory<EpicuriousIdentityContext>();
            //services.AddDbContextFactory<ApplicationDbContextFactory>();

            return services;
        }
    }
}
