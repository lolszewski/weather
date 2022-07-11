using Microsoft.Extensions.Configuration;

namespace Weather.DataAccess.Extensions;

public static class DataAccessConfigurationExtensions
{
    public static string GetWeatherDataItemsBlobNameTemplate(this IConfiguration configuration)
        => configuration["WeatherData:ItemsBlobNameTemplate"];

    public static string GetWeatherDataHistoricalItemsBlobNameTemplate(this IConfiguration configuration)
        => configuration["WeatherData:HistoricalItemsBlobNameTemplate"];

    public static string GetWeatherDataItemsBlobNameDateFormat(this IConfiguration configuration)
        => configuration["WeatherData:ItemsBlobNameDateFormat"];

    public static string GetWeatherMetadataBlobName(this IConfiguration configuration)
        => configuration["WeatherData:MetadataBlobName"];

    public static string GetWeatherDataLinesSeparator(this IConfiguration configuration)
        => configuration["WeatherData:LinesSeparator"];

    public static string GetWeatherDataLineDataItemValueSeparator(this IConfiguration configuration)
        => configuration["WeatherData:LineDataItemValueSeparator"];
}