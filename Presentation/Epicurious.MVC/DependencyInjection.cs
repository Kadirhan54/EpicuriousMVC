using Epicurious.Domain.Identity;
using Epicurious.Infrastructure.Contexts.Identity;
using Microsoft.AspNetCore.Identity;

namespace Epicurious.MVC
{
    public static class DependencyInjection
    {
        public static async Task<IServiceCollection> AddWebServicesAsync(this IServiceCollection services)
        {
            services.AddSession();

            services.AddMvc().AddNToastNotifyToastr();

            // Caching
            services.AddMemoryCache();

            // Add services for SignInManager
            services.AddScoped<SignInManager<User>>();

            services.AddIdentity<User, Role>(options =>
            {
                // User Password Options
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                // User Username and Email Options
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@$";
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<EpicuriousIdentityContext>();

            services.Configure<SecurityStampValidatorOptions>(options =>
            {
                options.ValidationInterval = TimeSpan.FromMinutes(30);
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/Auth/Login");
                options.LogoutPath = new PathString("/Auth/Logout");
                options.Cookie = new CookieBuilder
                {
                    Name = "YetgenJump",
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict,
                    SecurePolicy = CookieSecurePolicy.SameAsRequest // Always
                };
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = System.TimeSpan.FromDays(7);
                options.AccessDeniedPath = new PathString("/Auth/AccessDenied");
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdministratorRole",
                     policy => policy.RequireRole("Admin"));
            });

            // Create roles during application startup
            using (var serviceProvider = services.BuildServiceProvider())
            {
                var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>(); // Use your custom Role class
                var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

                // Check if roles exist, and create them if not
                if (!await roleManager.RoleExistsAsync("Admin"))
                {
                    await roleManager.CreateAsync(new Role { Name = "Admin" }); // Use your custom Role class
                }

                var user = await userManager.FindByEmailAsync("admin@example.com");
                await userManager.AddToRoleAsync(user, "Admin");
            }

            return services;
        }

    }
}
