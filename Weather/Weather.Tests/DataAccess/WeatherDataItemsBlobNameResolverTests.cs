using System;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using Weather.DataAccess;
using Xunit;

namespace Weather.Tests.DataAccess;

public class WeatherDataItemsBlobNameResolverTests
{
    private readonly IWeatherDataItemsBlobNameResolver _weatherDataItemsBlobNameResolver;
    
    public WeatherDataItemsBlobNameResolverTests()
    {
        var configurationMock = new Mock<IConfiguration>();
        configurationMock.SetupGet(m => m["WeatherData:ItemsBlobNameTemplate"]).Returns("{deviceId}/{sensorType}/{date}.txt");
        configurationMock.SetupGet(m => m["WeatherData:ItemsBlobNameDateFormat"]).Returns("yyyy-MM");

        _weatherDataItemsBlobNameResolver = new WeatherDataItemsBlobNameResolver(configurationMock.Object);
    }

    [Fact]
    public void Given_DeviceSensorAndDate_When_AllAreSet_Then_ReturnsProperBlobName()
    {
        var date = new DateOnly(2022, 12, 5);
        
        _weatherDataItemsBlobNameResolver.Resolve("mcmurdo", "temperature", date)
            .Should()
            .Be("mcmurdo/temperature/2022-12.txt");
    }
}