using AzureBlobProject.Models;

namespace AzureBlobProject.Services
{
    public interface IBlobService
    {
        Task<List<string>> GetAllBlobs(string containername);
        Task<List<BlobModel>> GetAllBlobsWithUrl(string containername);
        Task<string> GetBlob(string name, string containername);

        Task<bool> CreateBlob(string name, IFormFile file, string containerName, BlobModel blobModel);

        Task<bool> DeleteBlob(string name,string containerName);
    }
}
