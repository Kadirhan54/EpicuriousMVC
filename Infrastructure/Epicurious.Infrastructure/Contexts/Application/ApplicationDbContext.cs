using System.Reflection;
using Epicurious.Domain.Common;
using Epicurious.Domain.Entities;
using Microsoft.EntityFrameworkCore;



namespace Epicurious.Infrastructure.Contexts.Application
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Recipe> Recipes { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            //// ?? Error veriyo diye ekledik anlamadim
            //modelBuilder.Ignore<Domain.Identity.User>();
            //modelBuilder.Ignore<Role>();
            //modelBuilder.Ignore<UserSetting>();

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    ((ICreatedByEntity)entry.Entity).CreatedOn = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    ((IModifiedByEntity)entry.Entity).LastModifiedOn = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Deleted)
                {
                    ((IDeletedByEntity)entry.Entity).DeletedOn = DateTime.UtcNow;
                }
            }

            return base.SaveChanges();
        }
    }
}
