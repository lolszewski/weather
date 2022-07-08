namespace Weather.DataAccess.Entities;

public class WeatherMetadataItemEntity
{
    public WeatherMetadataItemEntity(string deviceId, string sensorType)
    {
        DeviceId = deviceId;
        SensorType = sensorType;
    }

    public string DeviceId { get; set; }

    public string SensorType { get; set; }
}