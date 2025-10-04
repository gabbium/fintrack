using Fintrack.ServiceName.Api;
using Fintrack.ServiceName.Application;
using Fintrack.ServiceName.Infrastructure;
using Fintrack.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddApplicationServices();
builder.AddInfrastructureServices();
builder.AddApiServices();

var app = builder.Build();

app.MapDefaultEndpoints();

app.UseApi();

await app.RunAsync();
