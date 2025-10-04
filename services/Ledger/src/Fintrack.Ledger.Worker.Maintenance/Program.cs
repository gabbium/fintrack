using Fintrack.Ledger.Infrastructure;
using Fintrack.Ledger.Worker.Maintenance;
using Fintrack.ServiceDefaults;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

builder.AddInfrastructureServices()
    .AddWorkerServices();

var host = builder.Build();

await host.RunAsync();
