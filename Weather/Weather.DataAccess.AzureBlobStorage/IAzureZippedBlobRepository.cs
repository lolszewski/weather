namespace Weather.DataAccess.AzureBlobStorage;

public interface IAzureZippedBlobRepository
{
    /// <summary>
    /// Gets unzipped blob.
    /// Dictionary's key is the file name.
    /// Dictionary's value is a file content.
    /// </summary>
    /// <param name="blobName">Name of the zipped blob</param>
    /// <returns>Dictionary containing unzipped files with its content.</returns>
    Task<Dictionary<string, string>> GetContent(string blobName);
}