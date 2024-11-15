using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using TsApi.Interfaces;

namespace TsApi.Endpoints;

public static class DataEndpoints
{
    public static RouteGroupBuilder MapDataEndpoints(this RouteGroupBuilder group)
    {
        var dataGroup = group.MapGroup("/data")
            .WithTags("data");

        dataGroup.MapGet("/", async (
            int signalId, 
            DateTime? from, 
            DateTime? to,
            IDataRepository data) => 
        {
            var timeSeriesData = await data.GetDataAsync(signalId, from, to);
            return Results.Ok(timeSeriesData);
        });

        return group;
    }
} 