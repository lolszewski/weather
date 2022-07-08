using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Weather.Contracts.Resources;

namespace Weather.Business.Managers;

public interface IWeatherDataItemsManager
{
    Task<IEnumerable<WeatherDataItemResource>> GetData(string deviceId, string sensorType, DateOnly date, int skip = 0, int take = 100);

    Task<IEnumerable<WeatherDataItemResource>> GetData(string deviceId, DateOnly date, int skip = 0, int take = 100);
}