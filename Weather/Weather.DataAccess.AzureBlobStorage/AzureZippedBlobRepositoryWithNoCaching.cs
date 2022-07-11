using System.Collections.Concurrent;
using System.IO.Compression;
using Azure.Storage.Blobs;

namespace Weather.DataAccess.AzureBlobStorage;

public class AzureZippedBlobRepositoryWithNoCaching : IAzureZippedBlobRepositoryWithNoCaching
{
    private readonly BlobContainerClient _blobContainerClient;

    public AzureZippedBlobRepositoryWithNoCaching(IAzureBlobContainerClientFactory azureBlobContainerClientFactory)
    {
        _blobContainerClient = azureBlobContainerClientFactory.CreateClient();
    }

    public async Task<Dictionary<string, string>> GetContent(string blobName)
    {
        var result = new Dictionary<string, string>();

        var zip = await _blobContainerClient.GetBlobClient(blobName).DownloadStreamingAsync();
        using (var archive = new ZipArchive(zip.Value.Content, ZipArchiveMode.Read))
        {
            foreach (var entry in archive.Entries)
            {
                await using var unzippedEntryStream = entry.Open();
                using var streamReader = new StreamReader(unzippedEntryStream);
                var content = await streamReader.ReadToEndAsync();

                result.Add(entry.Name, content);
            }
        }

        return result;
    }
}