using Epicurious.Domain.Identity;
using Epicurious.Infrastructure.Contexts.Identity;
using Microsoft.AspNetCore.Identity;

namespace Epicurious.MVC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services)
        {
            services.AddSession();

            //services.AddMvc().AddNToastNotifyToastr(new ToastrOptions()
            //{
            //    ProgressBar = false,
            //    PositionClass = ToastPositions.BottomCenter
            //});
            //Or simply go 
            services.AddMvc().AddNToastNotifyToastr();

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

            // Add services for SignInManager
            services.AddScoped<SignInManager<User>>();

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

            return services;
        }
    }
}
