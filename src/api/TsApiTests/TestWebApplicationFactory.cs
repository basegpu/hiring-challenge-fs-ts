using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TsApi;
using TsApi.Interfaces;
using TsApi.Repositories;

namespace TsApiTest;

public class TestWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remove any existing repository registrations
            var descriptors = services
                .Where(d => d.ServiceType == typeof(IAssetRepository)
                        || d.ServiceType == typeof(ISignalRepository)
                        || d.ServiceType == typeof(IDataRepository))
                .ToList();

            foreach (var descriptor in descriptors)
            {
                services.Remove(descriptor);
            }

            // Register in-memory repositories for testing
            services.AddSingleton<IAssetRepository, MemoryAssetRepository>();
            services.AddSingleton<ISignalRepository, MemorySignalRepository>();
            services.AddSingleton<IDataRepository, MemoryDataRepository>();
        });

        return base.CreateHost(builder);
    }
} 