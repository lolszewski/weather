using System.Linq;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using Weather.DataAccess.Mappers;
using Weather.Tests.DataAccess.TestData;
using Xunit;

namespace Weather.Tests.DataAccess.Mappers;

public class WeatherMetadataBlobContentToEntitiesMapperTests
{
    private readonly IWeatherMetadataBlobContentToEntitiesMapper _mapper;
    
    public WeatherMetadataBlobContentToEntitiesMapperTests()
    {
        var configurationMock = new Mock<IConfiguration>();
        configurationMock.Setup(m => m["WeatherData:LinesSeparator"]).Returns("\r\n");
        configurationMock.Setup(m => m["WeatherData:LineDataItemValueSeparator"]).Returns(";");

        _mapper = new WeatherMetadataBlobContentToEntitiesMapper(configurationMock.Object);
    }

    [Fact]
    public void Given_MetadataRealData_When_MapsToEntities_EntitiesAreReturned()
    {
        _mapper
            .Map(MetadataBlobContent.Value)
            .ToList()
            .Should()
            .NotBeEmpty();
    }
}