using System.Reflection;
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


    }
}
