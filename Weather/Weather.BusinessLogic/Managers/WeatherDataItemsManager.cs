using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weather.Business.Mappers;
using Weather.Contracts.Resources;
using Weather.DataAccess.Repositories;

namespace Weather.Business.Managers;

public class WeatherDataItemsManager : IWeatherDataItemsManager
{
    private readonly IWeatherDataItemsRepository _dataItemsRepository;
    private readonly IWeatherMetadataItemsRepository _metadataItemsRepository;
    private readonly IWeatherDataItemEntityToResourceMapper _mapper;

    public WeatherDataItemsManager(IWeatherDataItemsRepository dataItemsRepository, 
        IWeatherMetadataItemsRepository metadataItemsRepository, 
        IWeatherDataItemEntityToResourceMapper mapper)
    {
        _dataItemsRepository = dataItemsRepository;
        _metadataItemsRepository = metadataItemsRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<WeatherDataItemResource>> GetData(string deviceId, string sensorType, DateOnly date)
    {
        var entities = await _dataItemsRepository.GetDataItems(deviceId, sensorType, date);
        return entities.Select(_mapper.Map);
    }

    public async Task<IEnumerable<WeatherDataItemResource>> GetData(string deviceId, DateOnly date)
    {
        var metadata = await _metadataItemsRepository.GetData();
        var sensorTypes = metadata.Where(m => m.DeviceId == deviceId).Select(m => m.SensorType);

        var tasks = sensorTypes.Select(sensorType => _dataItemsRepository.GetDataItems(deviceId, sensorType, date)).ToArray();
        await Task.WhenAll(tasks);

        return tasks
            .Select(t => t.Result)
            .SelectMany(t => t)
            .Select(t => _mapper.Map(t));
    }
}