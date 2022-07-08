using Weather.DataAccess.Entities;

namespace Weather.DataAccess.Mappers;

public interface IWeatherDataItemsBlobContentToEntitiesMapper
{
    IEnumerable<WeatherDataItemEntity> Map(string deviceId, string sensorType, string content);
}