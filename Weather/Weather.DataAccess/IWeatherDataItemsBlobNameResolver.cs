namespace Weather.DataAccess;

public interface IWeatherDataItemsBlobNameResolver
{
    string Resolve(string deviceId, string sensorType, DateOnly date);
}