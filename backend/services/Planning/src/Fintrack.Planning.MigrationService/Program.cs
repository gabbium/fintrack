using Fintrack.Planning.Infrastructure;
using Fintrack.Planning.MigrationService;
using Fintrack.ServiceDefaults;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

builder.AddInfrastructureServices()
    .AddWorkerServices();

var host = builder.Build();

await host.RunAsync();
