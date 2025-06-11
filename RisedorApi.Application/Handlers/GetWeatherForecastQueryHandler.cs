using MediatR;
using RisedorApi.Application.Queries;

namespace RisedorApi.Application.Handlers;

public class GetWeatherForecastQueryHandler
    : IRequestHandler<GetWeatherForecastQuery, IEnumerable<WeatherForecast>>
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing",
        "Bracing",
        "Chilly",
        "Cool",
        "Mild",
        "Warm",
        "Balmy",
        "Hot",
        "Sweltering",
        "Scorching"
    };

    public Task<IEnumerable<WeatherForecast>> Handle(
        GetWeatherForecastQuery request,
        CancellationToken cancellationToken
    )
    {
        var forecast = Enumerable
            .Range(1, 5)
            .Select(
                index =>
                    new WeatherForecast(
                        DateTime.Now.AddDays(index),
                        Random.Shared.Next(-20, 55),
                        Summaries[Random.Shared.Next(Summaries.Length)]
                    )
            );

        return Task.FromResult(forecast);
    }
}
