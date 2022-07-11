using System.Collections.Concurrent;

namespace Weather.DataAccess.AzureBlobStorage;

public class AzureZippedBlobRepositoryWithCaching : IAzureZippedBlobRepositoryWithCaching
{
    private readonly IAzureZippedBlobRepositoryWithNoCaching _azureZippedBlobRepositoryWithNoCaching;
    private readonly ConcurrentDictionary<string, Dictionary<string, string>> _cache;

    public AzureZippedBlobRepositoryWithCaching(IAzureZippedBlobRepositoryWithNoCaching azureZippedBlobRepositoryWithNoCaching)
    {
        _azureZippedBlobRepositoryWithNoCaching = azureZippedBlobRepositoryWithNoCaching;
        _cache = new();
    }

    public async Task<Dictionary<string, string>> GetContent(string blobName)
    {
        if (!_cache.TryGetValue(blobName, out var result))
        {
            result = await _azureZippedBlobRepositoryWithNoCaching.GetContent(blobName);
            
            _cache.TryAdd(blobName, result);
        }

        return result;
    }
}