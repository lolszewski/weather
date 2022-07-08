using Microsoft.Extensions.DependencyInjection;

namespace Weather.DataAccess.AzureBlobStorage.Extensions;

public static class AzureBlobStorageServiceCollectionExtensions
{
    public static void AddAzureBlobStorageServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IAzureBlobContainerClientFactory, AzureBlobContainerClientFactory>();
        serviceCollection.AddScoped<IAzureBlobRepository, AzureBlobRepository>();
    }
}