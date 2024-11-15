using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using TsApi.Interfaces;

namespace TsApi.Endpoints;

public static class SignalEndpoints
{
    public static RouteGroupBuilder MapSignalEndpoints(this RouteGroupBuilder group)
    {
        var signalsGroup = group.MapGroup("/signals")
            .WithTags("signals");

        signalsGroup.MapGet("/", async (int? assetId, ISignalRepository repository) => 
            Results.Ok(assetId == null 
                ? await repository.GetAllAsync()
                : await repository.GetByAssetIdAsync(assetId.Value)));

        signalsGroup.MapGet("/{id}", async (int id, ISignalRepository repository) => {
            var signal = await repository.GetByIdAsync(id);
            return signal is null ? Results.NotFound() : Results.Ok(signal);
        });

        return group;
    }
} 