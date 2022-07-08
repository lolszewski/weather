using Weather.DataAccess.Entities;

namespace Weather.DataAccess.Repositories;

public interface IWeatherDataItemsRepository
{
    Task<IEnumerable<WeatherDataItemEntity>> GetDataItems(string deviceId, string sensorType, DateOnly date, int skip = 0, int take = 100);
}