using Weather.DataAccess.Entities;

namespace Weather.DataAccess.Mappers;

public interface IWeatherMetadataBlobContentToEntitiesMapper
{
    IEnumerable<WeatherMetadataItemEntity> Map(string content);
}