using Fintrack.ServiceDefaults;
using Fintrack.Ledger.API;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddApiVersioningDefaults();

builder.AddOpenApiDefaults();

builder.AddWebServices();

var app = builder.Build();

app.MapOpenApiEndpoints();

app.MapHealthCheckEndpoints();

app.UseWebApp();

await app.RunAsync();
