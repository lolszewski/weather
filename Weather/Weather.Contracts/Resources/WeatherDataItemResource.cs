using System.ComponentModel.DataAnnotations;

namespace Weather.Contracts.Resources;

/// <summary>
/// Weather data item resource
/// </summary>
public class WeatherDataItemResource
{
    public WeatherDataItemResource(string deviceId, string sensorType, DateTimeOffset date, decimal value)
    {
        DeviceId = deviceId;
        SensorType = sensorType;
        Date = date;
        Value = value;
    }

    /// <summary>
    /// Device identifier
    /// </summary>
    [Required]
    public string DeviceId { get; }

    /// <summary>
    /// Sensor type
    /// </summary>
    [Required]
    public string SensorType { get; }

    /// <summary>
    /// Data item date
    /// </summary>
    [Required]
    public DateTimeOffset Date { get; }

    /// <summary>
    /// Data item value
    /// </summary>
    [Required]
    public decimal Value { get; }
}