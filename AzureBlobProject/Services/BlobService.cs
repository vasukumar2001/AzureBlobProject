using Azure.Storage.Blobs;
using AzureBlobProject.Models;

namespace AzureBlobProject.Services
{
    public class BlobService : IBlobService
    {
        private readonly BlobServiceClient _blobClient;

        public BlobService(BlobServiceClient bolbClient)
        {
            _blobClient = bolbClient;
        }

        public async Task<bool> CreateBlob(string name, IFormFile file, string containerName, BlobModel blobModel)
        {
            BlobContainerClient blobContainerClient = _blobClient.GetBlobContainerClient(containerName);
            var blobclient = blobContainerClient.GetBlobClient(name);
            var httpsheader = new Azure.Storage.Blobs.Models.BlobHttpHeaders()
            {
                ContentType=file.ContentType
            };
            var result = await blobclient.UploadAsync(file.OpenReadStream(), httpsheader);
            if(result != null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteBlob(string name, string containerName)
        {

            BlobContainerClient blobContainerClient = _blobClient.GetBlobContainerClient(containerName);
            var blobclient = blobContainerClient.GetBlobClient(name);
            return await blobclient.DeleteIfExistsAsync();
        }

        public async Task<List<string>> GetAllBlobs(string containername)
        {
            List<string> BlobNames = new List<string>();  
            BlobContainerClient blobContainerClient = _blobClient.GetBlobContainerClient(containername);
            await foreach (var blob in blobContainerClient.GetBlobsAsync())
            {
                BlobNames.Add(blob.Name);
            }
            return BlobNames;
        }

        public Task<List<BlobModel>> GetAllBlobsWithUrl(string containername)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetBlob(string name, string containername)
        {
            BlobContainerClient blobContainerClient = _blobClient.GetBlobContainerClient(containername);
             var blobclient =blobContainerClient.GetBlobClient(name);

            if (blobclient != null)
            {
                return blobclient.Uri.AbsoluteUri;
            }
            return "";
        }
    }
}
