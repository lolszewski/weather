using System.Linq;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using Weather.DataAccess.Mappers;
using Weather.Tests.DataAccess.TestData;
using Xunit;

namespace Weather.Tests.DataAccess.Mappers;

public class WeatherDataBlobContentToEntitiesMapperTests
{
    private readonly IWeatherDataItemsBlobContentToEntitiesMapper _mapper;
    private readonly string _deviceId = "dockan";
    private readonly string _sensorType = "rainfall";

    public WeatherDataBlobContentToEntitiesMapperTests()
    {
        var configurationMock = new Mock<IConfiguration>();
        configurationMock.Setup(m => m["WeatherData:LinesSeparator"]).Returns("\r\n");
        configurationMock.Setup(m => m["WeatherData:LineDataItemValueSeparator"]).Returns(";");

        _mapper = new WeatherDataItemsBlobContentToEntitiesMapper(configurationMock.Object);
    }

    [Fact]
    public void Given_HumidityRealData_When_MapsToEntities_EntitiesAreReturned()
    {
        _mapper
            .Map(_deviceId, _sensorType, HumidityTestsBlobContent.Value)
            .ToList()
            .Should()
            .NotBeEmpty();
    }

    [Fact]
    public void Given_TemperatureRealData_When_MapsToEntities_EntitiesAreReturned()
    {
        _mapper
            .Map(_deviceId, _sensorType, TemperatureTestsBlobContent.Value)
            .ToList()
            .Should()
            .NotBeEmpty();
    }

    [Fact]
    public void Given_RainfallRealData_When_MapsToEntities_EntitiesAreReturned()
    {
        _mapper
            .Map(_deviceId, _sensorType, RainfallTestsBlobContent.Value)
            .ToList()
            .Should()
            .NotBeEmpty();
    }

    [Fact]
    public void Given_DataWithNoLeadingPositiveNumberValue_When_MapsToEntities_EntitiesAreReturned()
    {
        var entities = _mapper.Map(_deviceId, _sensorType, "2019-01-10T00:01:15;,62").ToList();

        entities.Should().HaveCount(1);
        entities.First().Value.Should().Be(0.62M);
    }

    [Fact]
    public void Given_DataWithNoLeadingNegativeNumberValue_When_MapsToEntities_EntitiesAreReturned()
    {
        var entities = _mapper.Map(_deviceId, _sensorType, "2019-01-10T00:01:15;-,62").ToList();

        entities.Should().HaveCount(1);
        entities.First().Value.Should().Be(-0.62M);
    }
}