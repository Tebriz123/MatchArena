using AutoMapper;
using MatchArena.Application;
using MatchArena.Domain.Entities;
using MatchArena.Domain.Settings.Stripes;
using MatchArena.Infrastructure;
using MatchArena.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Stripe;
using System;


var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<StripeSetting>(
    builder.Configuration.GetSection("Stripe")
);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();



builder = WebApplication.CreateBuilder(args);

// Lazımi servis qeydiyyatları
builder.Services.AddControllers();
builder.Services.AddAuthentication(); // Əgər authentication istifadə edirsənsə
builder.Services.AddAuthorization();  // Authorization üçün mütləq lazımdır








builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
builder.Services
    .AddAppilicationServices()
    .AddPersistenceServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration);


var app = builder.Build();




if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//using (var scope = app.Services.CreateScope())
//{
//    await app.UseAppDbContextInitializer(scope);
//}

app.UseHttpsRedirection();

app.UseAuthentication();


app.UseAuthorization();

app.MapControllers();

var mapper = app.Services.GetRequiredService<IMapper>();
//mapper.ConfigurationProvider.AssertConfigurationIsValid();

app.Run();
