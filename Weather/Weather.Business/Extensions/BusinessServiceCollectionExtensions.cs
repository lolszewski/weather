using Microsoft.Extensions.DependencyInjection;
using Weather.Business.Managers;
using Weather.Business.Mappers;

namespace Weather.Business.Extensions;

public static class BusinessServiceCollectionExtensions
{
    public static void AddBusinessServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IWeatherDataItemsManager, WeatherDataItemsManager>();
        serviceCollection.AddScoped<IWeatherDataItemEntityToResourceMapper, WeatherDataItemEntityToResourceMapper>();
    }
}