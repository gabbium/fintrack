using Fintrack.Planning.Application;
using Fintrack.Planning.Infrastructure;
using Fintrack.Planning.MigrationService;
using Fintrack.ServiceDefaults;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

builder.AddApplicationServices();
builder.AddInfrastructureServices();
builder.AddWorkerServices();

var host = builder.Build();

await host.RunAsync();
