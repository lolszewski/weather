using Weather.DataAccess.Entities;

namespace Weather.DataAccess.Repositories;

public interface IWeatherMetadataItemsRepository
{
    Task<IEnumerable<WeatherMetadataItemEntity>> GetData();
}