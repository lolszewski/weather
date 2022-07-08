using Weather.DataAccess.AzureBlobStorage;
using Weather.DataAccess.Entities;
using Weather.DataAccess.Mappers;

namespace Weather.DataAccess.Repositories;

public class WeatherDataItemsRepository : IWeatherDataItemsRepository
{
    private readonly IAzureBlobRepository _azureBlobRepository;
    private readonly IWeatherDataItemsBlobNameResolver _blobNameResolver;
    private readonly IWeatherDataItemsBlobContentToEntitiesMapper _mapper;

    public WeatherDataItemsRepository(IAzureBlobRepository azureBlobRepository, 
        IWeatherDataItemsBlobNameResolver blobNameResolver,
        IWeatherDataItemsBlobContentToEntitiesMapper mapper)
    {
        _azureBlobRepository = azureBlobRepository;
        _blobNameResolver = blobNameResolver;
        _mapper = mapper;
    }

    public async Task<IEnumerable<WeatherDataItemEntity>> GetDataItems(string deviceId, string sensorType, DateOnly date, int skip = 0, int take = 100)
    {
        var dataItems = new List<WeatherDataItemEntity>(take);
        var blobName = _blobNameResolver.Resolve(deviceId, sensorType, date);

        if (await _azureBlobRepository.Exists(blobName))
        {
            var content = await _azureBlobRepository.GetContent(blobName);
            dataItems.AddRange(_mapper.Map(deviceId, sensorType, content, skip, take));
        }

        return dataItems;
    }
}