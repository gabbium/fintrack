using Fintrack.Ledger.Api;
using Fintrack.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddApiServices();

var app = builder.Build();

app.MapDefaultEndpoints();

app.UseApi();

await app.RunAsync();
