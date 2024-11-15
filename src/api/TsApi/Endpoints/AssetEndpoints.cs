using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using TsApi.Interfaces;

namespace TsApi.Endpoints;

public static class AssetEndpoints
{
    public static RouteGroupBuilder MapAssetEndpoints(this RouteGroupBuilder group)
    {
        var assetsGroup = group.MapGroup("/assets")
            .WithTags("assets");

        assetsGroup.MapGet("/", async (IAssetRepository repository) => 
            Results.Ok(await repository.GetAllAsync()));

        assetsGroup.MapGet("/{id}", async (int id, IAssetRepository repository) => {
            var asset = await repository.GetByIdAsync(id);
            return asset is null ? Results.NotFound() : Results.Ok(asset);
        });

        return group;
    }
} 