using System.Reflection;
using Epicurious.Domain.Common;
using Epicurious.Domain.Entities;
using Epicurious.Domain.Identity;
using Microsoft.EntityFrameworkCore;



namespace Epicurious.Infrastructure.Contexts.Application
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<LikedRecipe> LikedRecipes { get; set; }
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

            // Configure the many-to-many relationship between User and LikedRecipe
            modelBuilder.Entity<LikedRecipe>()
                .HasKey(lr => new { lr.UserId, lr.RecipeId });

            modelBuilder.Entity<LikedRecipe>()
                .HasOne(lr => lr.User)
                .WithMany(u => u.LikedRecipes)
                .HasForeignKey(lr => lr.UserId);

            modelBuilder.Entity<LikedRecipe>()
                .HasOne(lr => lr.Recipe)
                .WithMany(r => r.LikedRecipes)
                .HasForeignKey(lr => lr.RecipeId);

            // Configure the many-to-many relationship between User and Recipe
            modelBuilder.Entity<User>()
                .HasMany(u => u.LikedRecipes)
                .WithOne(lr => lr.User)
                .HasForeignKey(lr => lr.UserId);

            modelBuilder.Entity<Recipe>()
                .HasMany(r => r.LikedRecipes)
                .WithOne(lr => lr.Recipe)
                .HasForeignKey(lr => lr.RecipeId);
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
