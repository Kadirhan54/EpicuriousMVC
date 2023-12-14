using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Epicurious.Infrastructure.Contexts.Identity
{
    public class YetgenIdentityContextFactory : IDesignTimeDbContextFactory<EpicuriousIdentityContext>
    {
        public EpicuriousIdentityContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<EpicuriousIdentityContext>();

            var connectionString = configuration.GetSection("EpicuriousIdentitySQLDB").Value;

            optionsBuilder.UseNpgsql(connectionString);

            return new EpicuriousIdentityContext(optionsBuilder.Options);
        }
    }
}
