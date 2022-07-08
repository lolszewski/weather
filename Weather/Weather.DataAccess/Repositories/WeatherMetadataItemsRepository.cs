using Microsoft.Extensions.Configuration;
using Weather.DataAccess.AzureBlobStorage;
using Weather.DataAccess.Entities;
using Weather.DataAccess.Extensions;
using Weather.DataAccess.Mappers;

namespace Weather.DataAccess.Repositories;

public class WeatherMetadataItemsRepository : IWeatherMetadataItemsRepository
{
    private readonly IAzureBlobRepository _blobRepository;
    private readonly IConfiguration _configuration;
    private readonly IWeatherMetadataBlobContentToEntitiesMapper _mapper;

    public WeatherMetadataItemsRepository(IAzureBlobRepository blobRepository, 
        IConfiguration configuration,
        IWeatherMetadataBlobContentToEntitiesMapper mapper)
    {
        _blobRepository = blobRepository;
        _configuration = configuration;
        _mapper = mapper;
    }

    public async Task<IEnumerable<WeatherMetadataItemEntity>> GetData()
    {
        var content = await _blobRepository.GetContent(_configuration.GetWeatherMetadataBlobName());
        return _mapper.Map(content);
    }
}