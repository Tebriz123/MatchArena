using MatchArena.Application.Interfaces.Repositories;
using MatchArena.Application.Interfaces.Services;
using MatchArena.Domain.Entities;
using MatchArena.Domain.Settings.Stripes;
using MatchArena.Persistence.Contexts;
using MatchArena.Persistence.Implementations.Repositories;
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


            services.AddScoped<IPlayerRepository, PlayerRepository>();
            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddScoped<IFieldRepository, FieldRepository>();
            services.AddScoped<ITournamentRepository, TournamentRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IColorRepository, ColorRepository>();
            services.AddScoped<ISizeRepository, SizeRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IReservationRepository, ReservationRepository>();
            services.AddScoped<ITournamentRegistrationRepository, TournamentRegistrationRepository>();
            services.AddScoped<IInviteRepository, InviteRepository>();
            services.AddScoped<IPlayerRatingRepository, PlayerRatingRepository>();
            services.AddScoped<IFieldRatingRepository, FieldRatingRepository>();
            services.AddScoped<IProductRatingRepository, ProductRatingRepository>();



            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IAppDbContextInitializer, AppDbContextInitializer>();
            services.AddScoped<IPlayerService, PlayerService>();
            services.AddScoped<ITeamService, TeamService>();
            services.AddScoped<IFieldService, FieldService>();
            services.AddScoped<ITournamentService, TournamentService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IColorService, ColorService>();
            services.AddScoped<ISizeService, SizeService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IReservationService, ReservationService>();
            services.AddScoped<ITournamentRegistrationService, TournamentRegistrationService>();
            services.AddScoped<IPlayerRatingService, PlayerRatingService>();
            services.AddScoped<IFieldRatingService, FieldRatingService>();
            services.AddScoped<IProductRatingService, ProductRatingService>();

            return services;
        }


        //public static async Task<IApplicationBuilder> UseAppDbContextInitializer(this IApplicationBuilder app,IServiceScope scope)
        //{
           
        //        var initializer = scope.ServiceProvider.GetRequiredService<AppDbContextInitializer>();

        //        await initializer.InitializeDbContext();
        //        await initializer.InitializeRoleAsync();
        //        await initializer.InitializeAdmin();

        //    return app;

        //}

    }
}
