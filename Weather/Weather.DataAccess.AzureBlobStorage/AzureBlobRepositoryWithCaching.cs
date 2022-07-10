using System.Collections.Concurrent;

namespace Weather.DataAccess.AzureBlobStorage;

public class AzureBlobRepositoryWithCaching : IAzureBlobRepositoryWithCaching
{
    private readonly IAzureBlobRepositoryWithNoCaching _azureBlobRepositoryWithNoCaching;
    private readonly ConcurrentDictionary<string, string> _cache;

    public AzureBlobRepositoryWithCaching(IAzureBlobRepositoryWithNoCaching azureBlobRepositoryWithNoCaching)
    {
        _azureBlobRepositoryWithNoCaching = azureBlobRepositoryWithNoCaching;
        _cache = new ConcurrentDictionary<string, string>();
    }

    public async Task<string> GetContent(string blobName)
    {
        if (!_cache.TryGetValue(blobName, out var content))
        {
            content = await _azureBlobRepositoryWithNoCaching.GetContent(blobName);
            _cache.TryAdd(blobName, content);
        }

        return content;
    }

    public async Task<bool> Exists(string blobName) => _cache.ContainsKey(blobName) || await _azureBlobRepositoryWithNoCaching.Exists(blobName);
}