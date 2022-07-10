using System.Collections.Concurrent;
using System.IO.Compression;
using Azure;
using Azure.Storage.Blobs;

namespace Weather.DataAccess.AzureBlobStorage;

public class AzureZippedBlobRepository : IAzureZippedBlobRepository
{
    private readonly BlobContainerClient _blobContainerClient;
    private readonly ConcurrentDictionary<string, Dictionary<string, string>> _cache;

    public AzureZippedBlobRepository(IAzureBlobContainerClientFactory azureBlobContainerClientFactory)
    {
        _blobContainerClient = azureBlobContainerClientFactory.CreateClient();
        _cache = new();
    }

    public async Task<Dictionary<string, string>> GetContent(string blobName)
    {
        if (!_cache.TryGetValue(blobName, out var result))
        {
            result = new();

            var zip = await _blobContainerClient.GetBlobClient(blobName).DownloadStreamingAsync();
            using (var stream = zip.Value.Content)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await stream.CopyToAsync(memoryStream);

                    var archive = new ZipArchive(memoryStream);

                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {
                        using (var unzippedEntryStream = entry.Open())
                        {
                            using (var streamReader = new StreamReader(unzippedEntryStream))
                            {
                                var content = await streamReader.ReadToEndAsync();

                                result.Add(entry.Name, content);
                            }
                        }
                    }
                }
            }

            _cache.TryAdd(blobName, result);
        }

        return result;
    }
}