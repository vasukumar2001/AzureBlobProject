using System;
using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TangyAzureFunc.Data;
using TangyAzureFunc.Models;

namespace TangyAzureFunc;

public class OnQueueTriggerUpDateDatabase
{
    private readonly ILogger<OnQueueTriggerUpDateDatabase> _logger;

    private readonly ApplicationDbContext _dbContext;
    public OnQueueTriggerUpDateDatabase(ILogger<OnQueueTriggerUpDateDatabase> logger,ApplicationDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [Function(nameof(OnQueueTriggerUpDateDatabase))]
    public void Run([QueueTrigger("SaleRequestInBound")] QueueMessage message)
    {
        try
        {
            string body = message.Body.ToString();
            SalesRequest? salesRequest = JsonConvert.DeserializeObject<SalesRequest>(body);
            if (salesRequest != null)
            {
                salesRequest.Status = "";
                _dbContext.SalesRequests.Add(salesRequest);
                _dbContext.SaveChanges();
            }
            _logger.LogInformation("C# Queue trigger function processed: {messageText}", message.MessageText);
        }catch(Exception a)
        {
            _logger.LogError(a.Message);
        }
    }
}