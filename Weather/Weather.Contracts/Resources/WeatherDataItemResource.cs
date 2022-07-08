namespace Weather.Contracts.Resources;

public class WeatherDataItemResource
{
    public WeatherDataItemResource(string deviceId, string sensorType, DateTimeOffset date, decimal value)
    {
        DeviceId = deviceId;
        SensorType = sensorType;
        Date = date;
        Value = value;
    }

    public string DeviceId { get; }
    public string SensorType { get; }
    public DateTimeOffset Date { get; }
    public decimal Value { get; }
}