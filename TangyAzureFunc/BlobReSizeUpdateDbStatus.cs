using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore; // <-- Use EF Core
using System.IO;
using System.Threading.Tasks;
using TangyAzureFunc.Data;
using TangyAzureFunc.Models;

namespace TangyAzureFunc;

public class BlobReSizeUpdateDbStatus
{
    private readonly ILogger<BlobReSizeUpdateDbStatus> _logger;
    private readonly ApplicationDbContext _dbContext;

    public BlobReSizeUpdateDbStatus(ILogger<BlobReSizeUpdateDbStatus> logger,ApplicationDbContext applicationDbContext)
    {
        _dbContext = applicationDbContext;
        _logger = logger;
    }

    [Function(nameof(BlobReSizeUpdateDbStatus))]
    public async Task Run([BlobTrigger("functionsalesrep-final/{name}", Connection = "")] Byte[] myBlobByte, string name)
    {
        try
        {
            string fileName = Path.GetFileNameWithoutExtension(name);

            SalesRequest salesRequest = await _dbContext.SalesRequests.FirstOrDefaultAsync(s => s.Id == fileName).ConfigureAwait(false);
            if (salesRequest != null)
            {
                salesRequest.Status = "Image Processed";
                await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            }


            _logger.LogInformation("C# Blob trigger function Processed for updateDBStatus \n Name: {name}", name);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing blob for updateDBStatus: {name}", name);
        }
    }
}