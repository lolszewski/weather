using Microsoft.Extensions.Configuration;
using Weather.DataAccess.Entities;
using Weather.DataAccess.Extensions;

namespace Weather.DataAccess.Mappers;

public class WeatherMetadataBlobContentToEntitiesMapper : IWeatherMetadataBlobContentToEntitiesMapper
{
    private readonly IConfiguration _configuration;

    public WeatherMetadataBlobContentToEntitiesMapper(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IEnumerable<WeatherMetadataItemEntity> Map(string content) => 
        content
            .Split(_configuration.GetWeatherDataLinesSeparator())
            .Select(ToWeatherDataItemEntity);

    private WeatherMetadataItemEntity ToWeatherDataItemEntity(string line)
    {
        var items = line.Split(_configuration.GetWeatherDataLineDataItemValueSeparator());

        return new WeatherMetadataItemEntity(items[0], items[1]);
    }
}