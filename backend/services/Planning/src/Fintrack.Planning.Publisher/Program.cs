using Fintrack.Planning.Application;
using Fintrack.Planning.Infrastructure;
using Fintrack.Planning.Publisher;
using Fintrack.ServiceDefaults;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

builder.AddApplicationServices();
builder.AddInfrastructureServices();
builder.AddPublisherServices();

var host = builder.Build();

await host.RunAsync();
