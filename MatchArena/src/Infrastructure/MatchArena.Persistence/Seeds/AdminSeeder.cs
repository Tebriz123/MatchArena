using MatchArena.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Persistence.Seeds
{
    public static class AdminSeeder
    {
        public static async Task SeedAdminAsync(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using var scope = serviceProvider.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

            var adminSettings = configuration.GetSection("AdminSettings");
            string userName = adminSettings["userName"]!;
            string email = adminSettings["email"]!;
            string password = adminSettings["password"]!;


            if (await userManager.FindByEmailAsync(email) is null)
            {
                var admin = new AppUser
                {
                    UserName = userName,
                    Name = userName,
                    Surname = userName,
                    Email = email,
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(admin, password);
                await userManager.AddToRoleAsync(admin, UserRole.Admin.ToString());
            }
        }
    }
}
