using Weather.DataAccess.AzureBlobStorage;
using Weather.DataAccess.Entities;
using Weather.DataAccess.Mappers;

namespace Weather.DataAccess.Repositories;

public class WeatherDataItemsRepository : IWeatherDataItemsRepository
{
    private readonly IAzureBlobRepository _azureBlobRepository;
    private readonly IAzureZippedBlobRepository _azureZippedBlobRepository;
    private readonly IWeatherDataItemsBlobNameResolver _blobNameResolver;
    private readonly IWeatherDataHistoricalItemsBlobNameResolver _historicalDataItemsBlobNameResolver;
    private readonly IWeatherDataItemsBlobContentToEntitiesMapper _mapper;

    public WeatherDataItemsRepository(IAzureBlobRepository azureBlobRepository, 
        IAzureZippedBlobRepository azureZippedBlobRepository,
        IWeatherDataItemsBlobNameResolver blobNameResolver,
        IWeatherDataHistoricalItemsBlobNameResolver historicalDataItemsBlobNameResolver,
        IWeatherDataItemsBlobContentToEntitiesMapper mapper)
    {
        _azureBlobRepository = azureBlobRepository;
        _azureZippedBlobRepository = azureZippedBlobRepository;
        _blobNameResolver = blobNameResolver;
        _historicalDataItemsBlobNameResolver = historicalDataItemsBlobNameResolver;
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
        else
        {
            var historicalDataItemsBlobName = _historicalDataItemsBlobNameResolver.Resolve(deviceId, sensorType);
            if (await _azureBlobRepository.Exists(historicalDataItemsBlobName))
            {
                var historicalFilesContent = await _azureZippedBlobRepository.GetContent(historicalDataItemsBlobName);

                if (historicalFilesContent.Any(h => blobName.EndsWith(h.Key)))
                {
                    var historicalFileContent = historicalFilesContent.First(h => blobName.EndsWith(h.Key));
                    dataItems.AddRange(_mapper.Map(deviceId, sensorType, historicalFileContent.Value, skip, take));
                }
            }
        }

        return dataItems;
    }
}