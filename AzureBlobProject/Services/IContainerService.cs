namespace AzureBlobProject.Services
{
    public interface IContainerService
    {
        Task<List<string>> GetAllContainerAndBlob();
        Task<List<string>> GetAllContainer();

        Task CreateContainer(string ContainerName);

        Task DeleteContainer(string ContainerName);
    }
}
