using Microsoft.Extensions.DependencyInjection;
using Weather.DataAccess.Mappers;
using Weather.DataAccess.Repositories;

namespace Weather.DataAccess.Extensions;

public static class DataAccessServiceCollectionExtensions
{
    public static void AddDataAccessServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IWeatherDataItemsBlobNameResolver, WeatherDataItemsBlobNameResolver>();
        serviceCollection.AddScoped<IWeatherDataHistoricalItemsBlobNameResolver, WeatherDataHistoricalItemsBlobNameResolver>();
        serviceCollection.AddScoped<IWeatherDataItemsBlobContentToEntitiesMapper, WeatherDataItemsBlobContentToEntitiesMapper>();
        serviceCollection.AddScoped<IWeatherMetadataBlobContentToEntitiesMapper, WeatherMetadataBlobContentToEntitiesMapper>();
        serviceCollection.AddScoped<IWeatherDataItemsRepository, WeatherDataItemsRepository>();
        serviceCollection.AddScoped<IWeatherMetadataItemsRepository, WeatherMetadataItemsRepository>();
    }
}