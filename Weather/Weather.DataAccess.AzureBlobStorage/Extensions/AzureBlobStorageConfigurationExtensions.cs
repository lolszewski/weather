using Microsoft.Extensions.Configuration;

namespace Weather.DataAccess.AzureBlobStorage.Extensions;

public static class AzureBlobStorageConfigurationExtensions
{
    public static string GetAzureBlobServiceConnectionString(this IConfiguration configuration) => configuration["AzureStorageSettings:ConnectionString"];

    public static string GetAzureBlobServiceContainerName(this IConfiguration configuration) => configuration["AzureStorageSettings:ContainerName"];
    
}