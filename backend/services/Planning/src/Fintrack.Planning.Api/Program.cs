using Fintrack.Planning.Api;
using Fintrack.Planning.Application;
using Fintrack.Planning.Infrastructure;
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
