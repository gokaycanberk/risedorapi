using MediatR;

namespace RisedorApi.Application.Queries;

public record GetWeatherForecastQuery() : IRequest<IEnumerable<WeatherForecast>>;

public record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
