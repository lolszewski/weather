using Azure.Storage.Blobs;

using Microsoft.Extensions.Configuration;

namespace Weather.Infrastructure
{
    public interface IBlobContainerClientFactory
    {
        BlobContainerClient CreateClient();
    }

    public class BlobContainerClientFactory : IBlobContainerClientFactory
    {
        private string _conectionString;
        private string _container;

        public BlobContainerClientFactory(IConfiguration configuration)
        {
            _conectionString = configuration["AzureStorageSettings:ConectionString"];
            _container = configuration["AzureStorageSettings:Container"];
        }

        public BlobContainerClient CreateClient()
        {
            var blobServiceClient = new BlobServiceClient(_conectionString);
            var containerClient = blobServiceClient.GetBlobContainerClient(_container);
            return containerClient;
        }
    }
}
