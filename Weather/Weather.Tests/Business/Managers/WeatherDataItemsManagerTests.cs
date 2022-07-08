using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Weather.Business.Managers;
using Weather.Business.Mappers;
using Weather.Contracts.Resources;
using Weather.DataAccess.Entities;
using Weather.DataAccess.Repositories;
using Xunit;

namespace Weather.Tests.Business.Managers;

public class WeatherDataItemsManagerTests
{
    private readonly IWeatherDataItemsManager _manager;
    private readonly Mock<IWeatherDataItemsRepository> _dataItemsRepositoryMock = new();
    private readonly Mock<IWeatherMetadataItemsRepository> _metadataRepositoryMock = new();
    private readonly Mock<IWeatherDataItemEntityToResourceMapper> _mapperMock = new();
    private readonly string _deviceId = "amsterdam";
    private readonly string _sensorType = "temperature";
    private readonly DateOnly _date = new(2022, 12, 22);
    private readonly int _skip = 100;
    private readonly int _take = 200;

    public WeatherDataItemsManagerTests()
    {
        _dataItemsRepositoryMock
            .Setup(m => m.GetDataItems(_deviceId, _sensorType, _date, _skip, _take))
            .ReturnsAsync(new List<WeatherDataItemEntity> { new(_deviceId, _sensorType, new DateTimeOffset(new DateTime(_date.Year, _date.Month, _date.Day)), 14.2M) });

        _mapperMock
            .Setup(m => m.Map(It.IsAny<WeatherDataItemEntity>()))
            .Returns(new WeatherDataItemResource(_deviceId, _sensorType, new DateTimeOffset(new DateTime(_date.Year, _date.Month, _date.Day)), 14.2M) );

        _manager = new WeatherDataItemsManager(_dataItemsRepositoryMock.Object, _metadataRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Given_QueryParameters_When_GetsDataItemsByDeviceAndSensor_Then_CallsDataItemsRepository()
    {
        await _manager.GetData(_deviceId, _sensorType, _date, _skip, _take);

        _dataItemsRepositoryMock.Verify(m => m.GetDataItems(_deviceId, _sensorType, _date, _skip, _take), Times.Once());
    }

    [Fact]
    public async Task Given_QueryParameters_When_GetsDataItemsByDeviceAndSensor_Then_CallsMapper()
    {
        var resources = await _manager.GetData(_deviceId, _sensorType, _date, _skip, _take);

        _mapperMock.Verify(m => m.Map(It.IsAny<WeatherDataItemEntity>()), Times.Exactly(resources.Count()));
    }

    [Fact]
    public async Task Given_QueryParameters_When_GetsDataItemsByDevice_Then_CallsMetadataRepository()
    {
        await _manager.GetData(_deviceId, _date, _skip, _take);

        _metadataRepositoryMock.Verify(m => m.GetData(), Times.Once());
    }
}