using Microsoft.Extensions.Configuration;
using Weather.DataAccess.Extensions;

namespace Weather.DataAccess;

public class WeatherDataItemsBlobNameResolver : IWeatherDataItemsBlobNameResolver
{
    private readonly IConfiguration _configuration;

    public WeatherDataItemsBlobNameResolver(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string Resolve(string deviceId, string sensorType, DateOnly date)
        => _configuration.GetWeatherDataItemsBlobNameTemplate()
            .Replace("{deviceId}", deviceId)
            .Replace("{sensorType}", sensorType)
            .Replace("{date}", date.ToString(_configuration.GetWeatherDataItemsBlobNameDateFormat()));
}