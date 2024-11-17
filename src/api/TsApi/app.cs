using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
            var builder = WebApplication.CreateBuilder(args);

            // Add services
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Add DbContext
            builder.Services.AddDbContext<TsDbContext>(options =>
                options.UseNpgsql("Host=db;Port=5432;Database=timeseries;Username=postgres;Password=postgrespw"));

            // Register repositories
            builder.Services.AddScoped<IAssetRepository, DbAssetRepository>();
            builder.Services.AddScoped<ISignalRepository, DbSignalRepository>();
            builder.Services.AddScoped<IDataRepository, DbDataRepository>();

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
