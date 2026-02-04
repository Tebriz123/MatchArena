using MatchArena.Application.Interfaces.Services;
using MatchArena.Domain.Entities;
using MatchArena.Persistence.Contexts;
using MatchArena.Persistence.Implementations.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MatchArena.Persistence
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration config)
        {

            services.AddDbContext<AppDbContext>(opt =>
            opt.UseSqlServer(config.GetConnectionString("Default"))
            );

            services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.User.RequireUniqueEmail = true;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 0;
                opt.Lockout.AllowedForNewUsers = true;
                opt.Lockout.MaxFailedAccessAttempts = 3;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
            }).AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();




            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IAppDbContextInitializer, AppDbContextInitializer>();

            return services;
        }


        public static async Task<IApplicationBuilder> UseAppDbContextInitializer(this IApplicationBuilder app,IServiceScope scope)
        {
           
                var initializer = scope.ServiceProvider.GetRequiredService<AppDbContextInitializer>();

                await initializer.InitializeDbContext();
                await initializer.InitializeRoleAsync();
                await initializer.InitializeAdmin();

            return app;

        }

    }
}
