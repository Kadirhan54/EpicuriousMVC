using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Epicurious.Domain.Identity;
using Epicurious.Domain.Entities;

namespace Epicurious.Infrastructure.Contexts.Identity
{
    public class EpicuriousIdentityContext : IdentityDbContext<User, Role, Guid>
    {
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<LikedRecipe> LikedRecipes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }

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
