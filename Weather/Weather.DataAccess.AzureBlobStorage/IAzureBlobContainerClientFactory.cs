using Azure.Storage.Blobs;

namespace Weather.DataAccess.AzureBlobStorage;

public interface IAzureBlobContainerClientFactory
{
    BlobContainerClient CreateClient();
}