namespace Weather.DataAccess.Entities;

public class WeatherDataItemEntity
{
    public WeatherDataItemEntity(string deviceId, string sensorType, DateTimeOffset date, decimal value)
    {
        DeviceId = deviceId;
        SensorType = sensorType;
        Date = date;
        Value = value;
    }

    public string DeviceId { get; }
    public string SensorType { get; }
    public DateTimeOffset Date { get; init; }
    public decimal Value { get; init; }
}