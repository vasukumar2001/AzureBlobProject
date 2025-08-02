using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TangyAzureFunc.Data;
using TangyAzureFunc.Models;

namespace TangyAzureFunc;

public class UpdateStatusToComplete
{
    private readonly ILogger _logger;
    private readonly ApplicationDbContext _dbContext;

    public UpdateStatusToComplete(ILoggerFactory loggerFactory,ApplicationDbContext dbContext)
    {
        _logger = loggerFactory.CreateLogger<UpdateStatusToComplete>();
        _dbContext = dbContext;
    }

    [Function("UpdateStatusToComplete")]
    public async Task Run([TimerTrigger("0 */1 * * * *")] TimerInfo myTimer)
    {
        _logger.LogInformation("C# Timer trigger function executed at: {executionTime}", DateTime.Now);

        IEnumerable<SalesRequest> salesRequests = _dbContext.SalesRequests
            .Where(s => s.Status == "Image Processed").ToList();

        foreach (var salesRequest in salesRequests)
        {
            salesRequest.Status = "Completed";
        }
        await _dbContext.SaveChangesAsync().ConfigureAwait(false);

        if (myTimer.ScheduleStatus is not null)
        {
            _logger.LogInformation("Next timer schedule at: {nextSchedule}", myTimer.ScheduleStatus.Next);
        }
    }
}