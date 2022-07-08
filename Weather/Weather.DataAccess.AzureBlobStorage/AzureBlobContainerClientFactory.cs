using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using Weather.DataAccess.AzureBlobStorage.Extensions;

namespace Weather.DataAccess.AzureBlobStorage
{
    public class AzureBlobContainerClientFactory : IAzureBlobContainerClientFactory
    {
        private readonly IConfiguration _configuration;

        public AzureBlobContainerClientFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public BlobContainerClient CreateClient()
        {
            var blobServiceClient = new BlobServiceClient(_configuration.GetAzureBlobServiceConnectionString());
            var containerClient = blobServiceClient.GetBlobContainerClient(_configuration.GetAzureBlobServiceContainerName());
            return containerClient;
        }
    }
}
