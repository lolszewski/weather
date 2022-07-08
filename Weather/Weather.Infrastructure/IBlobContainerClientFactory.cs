using Azure.Storage.Blobs;

namespace Weather.Infrastructure;

public interface IBlobContainerClientFactory
{
    BlobContainerClient CreateClient();
}