using Fintrack.Ledger.Infrastructure;
using Fintrack.Ledger.MigrationService;
using Fintrack.ServiceDefaults;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

builder.AddInfrastructureServices();
builder.AddWorkerServices();

var host = builder.Build();

await host.RunAsync();
