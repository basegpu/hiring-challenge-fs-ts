using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Linq;
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
            bool? columns,
            IDataRepository data) => 
        {
            var timeSeriesData = await data.GetDataAsync(signalId, from, to);
            if (columns.HasValue && columns.Value)
            {
                return Results.Ok(new {
                    SignalId = signalId,
                    Timestamps = timeSeriesData.Select(d => d.Timestamp).ToArray(),
                    Values = timeSeriesData.Select(d => d.Value).ToArray()
                });
            }
            return Results.Ok(timeSeriesData);
        });

        return group;
    }
} 