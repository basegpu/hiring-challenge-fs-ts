using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
namespace TsApi.Endpoints;

public static class UtilsEndpoints
{
    public static RouteGroupBuilder MapUtilsEndpoints(this RouteGroupBuilder group)
    {
        var utilsGroup = group.MapGroup("/")
            .WithTags("utils");

        utilsGroup.MapGet("/health", () => Results.Ok());
        
        utilsGroup.MapGet("/version", () => Results.Ok(new { version = "1.0.0" }));

        return group;
    }
} 