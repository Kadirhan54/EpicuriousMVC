using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Epicurious.Domain.Identity;

namespace Epicurious.Infrastructure.Contexts.Identity
{
    public class EpicuriousIdentityContext : IdentityDbContext<User, Role, Guid>
    {
        public EpicuriousIdentityContext(DbContextOptions<EpicuriousIdentityContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
