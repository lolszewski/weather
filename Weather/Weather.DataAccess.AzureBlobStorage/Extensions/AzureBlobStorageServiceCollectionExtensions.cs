using Microsoft.Extensions.DependencyInjection;

namespace Weather.DataAccess.AzureBlobStorage.Extensions;

public static class AzureBlobStorageServiceCollectionExtensions
{
    public static void AddAzureBlobStorageServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IAzureBlobContainerClientFactory, AzureBlobContainerClientFactory>();
        
        serviceCollection.AddSingleton<IAzureBlobRepositoryWithNoCaching, AzureBlobRepositoryWithNoCaching>();
        serviceCollection.AddSingleton<IAzureBlobRepositoryWithCaching, AzureBlobRepositoryWithCaching>();
        serviceCollection.AddSingleton<IAzureBlobRepository, AzureBlobRepositoryWithCaching>();

        serviceCollection.AddSingleton<IAzureZippedBlobRepositoryWithNoCaching, AzureZippedBlobRepositoryWithNoCaching>();
        serviceCollection.AddSingleton<IAzureZippedBlobRepositoryWithCaching, AzureZippedBlobRepositoryWithCaching>();
        serviceCollection.AddSingleton<IAzureZippedBlobRepository, AzureZippedBlobRepositoryWithCaching>();
    }
}