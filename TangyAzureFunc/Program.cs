using Google.Protobuf.WellKnownTypes;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TangyAzureFunc.Data;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

// Application Insights isn't enabled by default. See https://aka.ms/AAt8mw4.
// builder.Services
//     .AddApplicationInsightsTelemetryWorkerService()
//     .ConfigureFunctionsApplicationInsights();

string ConnectionString = Environment.GetEnvironmentVariable("AzureSqlDatabase");

builder.Services.AddDbContext<ApplicationDbContext>(Option => Option.UseSqlServer(ConnectionString));

builder.Build().Run();
