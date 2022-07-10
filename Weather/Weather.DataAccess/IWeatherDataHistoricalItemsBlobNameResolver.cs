namespace Weather.DataAccess;

public interface IWeatherDataHistoricalItemsBlobNameResolver
{
    string Resolve(string deviceId, string sensorType);
}