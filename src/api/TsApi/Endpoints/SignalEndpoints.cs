using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
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

        signalsGroup.MapGet("/{id}/data", async (
            int id, 
            DateTime? from, 
            DateTime? to, 
            ISignalRepository signals,
            IDataRepository data) => 
        {
            var signal = await signals.GetByIdAsync(id);
            if (signal is null) return Results.NotFound();

            var timeSeriesData = await data.GetDataAsync(id, from, to);
            return Results.Ok(timeSeriesData);
        });

        return group;
    }
} 