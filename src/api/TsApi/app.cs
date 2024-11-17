using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using TsApi.Data;
using TsApi.Endpoints;
using TsApi.Interfaces;
using TsApi.Repositories;

namespace TsApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Create builder
            var builder = WebApplication.CreateBuilder(args);

            // Create logger
            var logger = LoggerFactory.Create(config => config.AddConsole())
                                    .CreateLogger<Program>();

            // Add services
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // try to readdatabase connection settings from environment variables
            var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
            var dbPort = Environment.GetEnvironmentVariable("DB_PORT");
            var dbName = Environment.GetEnvironmentVariable("DB_NAME");
            var dbUser = Environment.GetEnvironmentVariable("DB_USER");
            var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");

            // check if all required database connection settings are present
            if (string.IsNullOrEmpty(dbHost) || string.IsNullOrEmpty(dbPort) || string.IsNullOrEmpty(dbName) || string.IsNullOrEmpty(dbUser) || string.IsNullOrEmpty(dbPassword))
            {
                logger.LogWarning("Missing required database configuration. Please check environment variables: DB_HOST, DB_PORT, DB_NAME, DB_USER, DB_PASSWORD");
                logger.LogInformation("Using in-memory database");
                builder.Services.AddScoped<IAssetRepository, MemoryAssetRepository>();
                builder.Services.AddScoped<ISignalRepository, MemorySignalRepository>();
                builder.Services.AddScoped<IDataRepository, MemoryDataRepository>();
            }
            else
            {
                var connectionString = $"Host={dbHost};Port={dbPort};Database={dbName};Username={dbUser};Password={dbPassword}";
                builder.Services.AddDbContext<TsDbContext>(options =>
                    options.UseNpgsql(connectionString));
                // register repositories
                builder.Services.AddScoped<IAssetRepository, DbAssetRepository>();
                builder.Services.AddScoped<ISignalRepository, DbSignalRepository>();
                builder.Services.AddScoped<IDataRepository, DbDataRepository>();
            }

            var app = builder.Build();

            // Configure middleware
            app.UseSwagger();
            app.UseSwaggerUI();

            // Map endpoints
            app.MapGroup("api")
                .MapUtilsEndpoints()
                .MapAssetEndpoints()
                .MapSignalEndpoints()
                .MapDataEndpoints();

            app.Run();
        }
    }
}
