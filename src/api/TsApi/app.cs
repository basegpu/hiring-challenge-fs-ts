using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
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

            builder.Services.AddSingleton<IAssetRepository, MemoryAssetRepository>();
            builder.Services.AddSingleton<ISignalRepository, MemorySignalRepository>();
            builder.Services.AddSingleton<IDataRepository, MemoryDataRepository>();

            var app = builder.Build();

            // Configure middleware
            app.UseSwagger();
            app.UseSwaggerUI();

            // Map endpoints
            app.MapGroup("api")
                .MapUtilsEndpoints()
                .MapAssetEndpoints()
                .MapSignalEndpoints();

            app.Run();
        }
    }
}
