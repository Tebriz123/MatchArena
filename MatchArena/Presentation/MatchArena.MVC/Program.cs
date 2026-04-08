using MatchArena.MVC.Services;
using MatchArena.MVC.Services.Implementations;
using MatchArena.MVC.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace MatchArena.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<NotificationFilter>();

            builder.Services.AddControllersWithViews(options =>
            {
                options.Filters.AddService<NotificationFilter>();
            });

            builder.Services.AddHttpClient("MatchArenaClient", config =>
            {
                config.BaseAddress = new Uri("https://localhost:7246/");
                config.DefaultRequestHeaders.Add("accept", "application/json");
            });

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Login";
                    options.LogoutPath = "/Account/Logout";
                    options.AccessDeniedPath = "/Account/Login";
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                    options.SlidingExpiration = true;
                });

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<IProductClientService, ProductClientService>();
            builder.Services.AddScoped<IPlayerClientService, PlayerClientService>();
            builder.Services.AddScoped<IFieldClientService, FieldClientService>();
            builder.Services.AddScoped<ITeamClientService, TeamClientService>();
            builder.Services.AddScoped<ITournamentClientService, TournamentClientService>();
            builder.Services.AddScoped<ICategoryClientService, CategoryClientService>();
            builder.Services.AddScoped<IColorClientService, ColorClientService>();
            builder.Services.AddScoped<ISizeClientService, SizeClientService>();
            builder.Services.AddScoped<IAccountClientService, AccountClientService>();
            builder.Services.AddScoped<ITournamentRegistrationClientService, TournamentRegistrationClientService>();
            builder.Services.AddScoped<IReservationClientService, ReservationClientService>();
            builder.Services.AddScoped<IProductRatingClientService, ProductRatingClientService>();
            builder.Services.AddScoped<IPlayerRatingClientService, PlayerRatingClientService>();
            builder.Services.AddScoped<IFieldRatingClientService, FieldRatingClientService>();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}