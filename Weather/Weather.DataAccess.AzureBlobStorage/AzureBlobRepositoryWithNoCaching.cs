using Azure.Storage.Blobs;

namespace Weather.DataAccess.AzureBlobStorage;

public class AzureBlobRepositoryWithNoCaching : IAzureBlobRepositoryWithNoCaching
{
    private readonly BlobContainerClient _blobContainerClient;

    public AzureBlobRepositoryWithNoCaching(IAzureBlobContainerClientFactory azureBlobContainerClientFactory)
    {
        _blobContainerClient = azureBlobContainerClientFactory.CreateClient();
    }

    public async Task<string> GetContent(string blobName)
    {
        var downloadResult = await GetBlobClient(blobName).DownloadContentAsync();
        return downloadResult.Value.Content.ToString();
    }

    public async Task<bool> Exists(string blobName)
    {
        var exists = await GetBlobClient(blobName).ExistsAsync();
        return exists.Value;
    }

    private BlobClient GetBlobClient(string blobName) => 
        _blobContainerClient.GetBlobClient(blobName);
}