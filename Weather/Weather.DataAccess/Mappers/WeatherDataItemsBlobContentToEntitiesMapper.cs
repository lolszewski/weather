using Microsoft.Extensions.Configuration;
using Weather.DataAccess.Entities;
using Weather.DataAccess.Extensions;

namespace Weather.DataAccess.Mappers;

public class WeatherDataItemsBlobContentToEntitiesMapper : IWeatherDataItemsBlobContentToEntitiesMapper
{
    private readonly IConfiguration _configuration;

    public WeatherDataItemsBlobContentToEntitiesMapper(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IEnumerable<WeatherDataItemEntity> Map(string deviceId, string sensorType, string content, int skip = 0, int take = 100)
    {
        return content
            .Split(_configuration.GetWeatherDataLinesSeparator())
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .Skip(skip)
            .Take(take)
            .Select(l => ToWeatherDataItemEntity(deviceId, sensorType, l));
    }

    private WeatherDataItemEntity ToWeatherDataItemEntity(string deviceId, string sensorType, string line)
    {
        var items = line.Split(_configuration.GetWeatherDataLineDataItemValueSeparator());
        var dateString = SanitizeDateString(items[0]);
        var valueString = SanitizeDecimalString(items[1]);

        return new WeatherDataItemEntity(deviceId, sensorType, DateTimeOffset.Parse(dateString), decimal.Parse(valueString));
    }

    private string SanitizeDecimalString(string decimalString)
    {
        if (decimalString.StartsWith(","))
        {
            return SanitizeDecimalString($"0{decimalString}");
        }

        if (decimalString.StartsWith("-,"))
        {
            return decimalString.Replace("-,", "-0.");
        }
        
        return decimalString.Replace(",", ".");
    }

    /// <summary>
    /// Ensures that input date string will be interpreted as UTC date and there will be no offset.
    /// </summary>
    /// <param name="dateString">Input date string</param>
    /// <returns>Date string with "Z" ending indicating that date is in UTC format</returns>
    private string SanitizeDateString(string dateString)
    {
        if (!dateString.EndsWith("Z"))
        {
            return $"{dateString}Z";
        }

        return dateString;
    }
}