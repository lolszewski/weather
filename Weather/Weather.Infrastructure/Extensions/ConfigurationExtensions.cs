using Microsoft.Extensions.Configuration;

namespace Weather.Infrastructure.Extensions;

public static class ConfigurationExtensions
{
    public static string GetAzureBlobServiceConnectionString(this IConfiguration configuration) => configuration["AzureStorageSettings:ConnectionString"];

    public static string GetAzureBlobServiceContainerName(this IConfiguration configuration) => configuration["AzureStorageSettings:ContainerName"];
    
}