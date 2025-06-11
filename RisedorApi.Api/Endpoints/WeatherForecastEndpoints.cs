using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using RisedorApi.Application.Queries;

namespace RisedorApi.Api.Endpoints;

public static class WeatherForecastEndpoints
{
    public static void MapWeatherForecastEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet(
                "/weatherforecast",
                async (IMediator mediator) =>
                {
                    var query = new GetWeatherForecastQuery();
                    var result = await mediator.Send(query);
                    return Results.Ok(result);
                }
            )
            .WithName("GetWeatherForecast")
            .WithOpenApi();
    }
}
