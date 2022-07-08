using Weather.Contracts.Resources;
using Weather.DataAccess.Entities;

namespace Weather.Business.Mappers;

public interface IWeatherDataItemEntityToResourceMapper
{
    WeatherDataItemResource Map(WeatherDataItemEntity entity);
}