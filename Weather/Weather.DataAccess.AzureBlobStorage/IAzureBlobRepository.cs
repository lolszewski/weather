namespace Weather.DataAccess.AzureBlobStorage;

public interface IAzureBlobRepository
{
    Task<string> GetContent(string blobName);

    Task<bool> Exists(string blobName);
}