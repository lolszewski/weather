using Weather.Contracts.Resources;
using Weather.DataAccess.Entities;

namespace Weather.Business.Mappers;

public class WeatherDataItemEntityToResourceMapper : IWeatherDataItemEntityToResourceMapper
{
    public WeatherDataItemResource Map(WeatherDataItemEntity entity) => new(entity.DeviceId, entity.SensorType, entity.Date, entity.Value);
}