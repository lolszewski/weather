using Microsoft.Extensions.Configuration;
using Weather.DataAccess.Extensions;

namespace Weather.DataAccess;

public class WeatherDataHistoricalItemsBlobNameResolver : IWeatherDataHistoricalItemsBlobNameResolver
{
    private readonly IConfiguration _configuration;

    public WeatherDataHistoricalItemsBlobNameResolver(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string Resolve(string deviceId, string sensorType)
        => _configuration.GetWeatherDataHistoricalItemsBlobNameTemplate()
            .Replace("{deviceId}", deviceId)
            .Replace("{sensorType}", sensorType);
}