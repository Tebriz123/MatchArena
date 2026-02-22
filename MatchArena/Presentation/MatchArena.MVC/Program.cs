using MatchArena.MVC.Services.Implementations;
using MatchArena.MVC.Services.Interfaces;

namespace MatchArena.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddHttpClient("MatchArenaClient", config =>
            {
                config.BaseAddress = new Uri("https://localhost:7246/");
                config.DefaultRequestHeaders.Add("accept","application/json"); 
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

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

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
