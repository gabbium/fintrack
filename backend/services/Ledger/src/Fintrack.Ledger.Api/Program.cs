using Fintrack.Ledger.Api;
using Fintrack.Ledger.Application;
using Fintrack.Ledger.Infrastructure;
using Fintrack.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddApplicationServices();
builder.AddInfrastructureServices();
builder.AddApiServices();

var app = builder.Build();

app.MapServiceDefaultEndpoints();

app.UseApi();

await app.RunAsync();
