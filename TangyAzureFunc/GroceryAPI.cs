using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TangyAzureFunc.Data;
using TangyAzureFunc.Models;

namespace TangyAzureFunc;

public class GroceryAPI
{
    private readonly ILogger<GroceryAPI> _logger;
    private readonly ApplicationDbContext _dbContext;

    public GroceryAPI(ILogger<GroceryAPI> logger, ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    [Function("GetGrocery")]
    public async Task<IActionResult> GetGrocery([HttpTrigger(AuthorizationLevel.Function, "get", Route = "GroceryList")] HttpRequest req)
    {
        _logger.LogInformation("GetGrocery List");
        return new OkObjectResult(await _dbContext.GroceryItems.ToListAsync().ConfigureAwait(false));
    }

    [Function("GetGroceryById")]
    public async Task<IActionResult> GetGroceryById([HttpTrigger(AuthorizationLevel.Function, "get", Route = "GroceryList/{id}")] HttpRequest req,string id)
    {
        _logger.LogInformation("GetGrocery Item-" + id);
        return new OkObjectResult(await _dbContext.GroceryItems.FirstOrDefaultAsync(s=>s.Id==id).ConfigureAwait(false));
    }

    [Function("CreateGrocery")]
    public async Task<IActionResult> CreateGrocery([HttpTrigger(AuthorizationLevel.Function, "post", Route = "GroceryList")] HttpRequest req)
    {
        _logger.LogInformation("Create Grocery List item");
        string ReqBody = await new StreamReader(req.Body).ReadToEndAsync();
        GroceryItem_Upsert? groceryItem_Upsert = JsonConvert.DeserializeObject<GroceryItem_Upsert>(ReqBody);

        GroceryItem groceryItem = new GroceryItem
        {
            Name = groceryItem_Upsert?.Name ?? "Unknown Item"
        };
        await _dbContext.GroceryItems.AddAsync(groceryItem).ConfigureAwait(false);
        await _dbContext.SaveChangesAsync().ConfigureAwait(false);

        return new OkObjectResult(groceryItem);
    }

    [Function("UpdateGrocery")]
    public async Task<IActionResult> UpdateGrocery([HttpTrigger(AuthorizationLevel.Function, "put", Route = "GroceryList/{id}")] HttpRequest req,string id)
    {
        _logger.LogInformation("Update Grocery List item");

        var groceryItem = await _dbContext.GroceryItems.FirstOrDefaultAsync(s => s.Id == id).ConfigureAwait(false);
        if (groceryItem == null)
        {
            return new NotFoundObjectResult("Iteam Not Found.");
        }

        string ReqBody = await new StreamReader(req.Body).ReadToEndAsync();
        GroceryItem_Upsert? groceryItem_Upsert = JsonConvert.DeserializeObject<GroceryItem_Upsert>(ReqBody);
        if(!string.IsNullOrEmpty(groceryItem_Upsert?.Name))
        {
            groceryItem.Name = groceryItem_Upsert.Name;
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        return new OkObjectResult(groceryItem);
    }

    [Function("DeleteGrocery")]
    public async Task<IActionResult> DeleteGrocery([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "GroceryList/{id}")] HttpRequest req, string id)
    {
        _logger.LogInformation("Delete Grocery List item");

        var groceryItem = await _dbContext.GroceryItems.FirstOrDefaultAsync(s => s.Id == id).ConfigureAwait(false);
        if (groceryItem == null)
        {
            return new NotFoundObjectResult("Iteam Not Found.");
        }
        _dbContext.GroceryItems.Remove(groceryItem);
         await _dbContext.SaveChangesAsync().ConfigureAwait(false);

        return new OkResult();
    }

}