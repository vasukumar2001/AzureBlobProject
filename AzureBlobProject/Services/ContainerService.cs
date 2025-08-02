using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace AzureBlobProject.Services
{
    public class ContainerService:IContainerService
    {
        private readonly BlobServiceClient _blobClient;

        public ContainerService(BlobServiceClient bolbClient)
        {
            _blobClient = bolbClient;
        }

        public async Task CreateContainer(string Containername)
        {
            BlobContainerClient blobContainerClient =_blobClient.GetBlobContainerClient(Containername);
            await blobContainerClient.CreateIfNotExistsAsync(PublicAccessType.BlobContainer);
        }

        public async Task DeleteContainer(string Containername)
        {
            BlobContainerClient blobContainerClient = _blobClient.GetBlobContainerClient(Containername);
            await blobContainerClient.DeleteIfExistsAsync();
        }

       
        public async Task<List<string>> GetAllContainer()
        {
            List<string> containerNames = new List<string>();

            await foreach (BlobContainerItem iteam in _blobClient.GetBlobContainersAsync())
            {
                containerNames.Add(iteam.Name);
            }

            return containerNames;
        }

        public async Task<List<string>> GetAllContainerAndBlob()
        {
            List<string> containerandBlobName = new List<string>();

            containerandBlobName.Add("----- Account  Name : " + _blobClient.AccountName + " ------------");
            containerandBlobName.Add("-------------------------------------------------------------------");

            await foreach (BlobContainerItem iteam in _blobClient.GetBlobContainersAsync())
            {
                containerandBlobName.Add(iteam.Name);
            }
            return containerandBlobName;

        }   
    }
}
