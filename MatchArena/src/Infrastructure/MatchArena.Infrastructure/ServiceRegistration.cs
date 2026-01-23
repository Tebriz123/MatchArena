using MatchArena.Application.Interfaces.Services;
using MatchArena.Infrastructure.Implementations.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Infrastructure
{
    public static class ServiceRegistration 
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITokenService, TokenService>();

            services.AddAuthentication(opt =>
           {
               opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
               opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
           } );

            return services;
        }
    }
}
