using Fintrack.Ledger.API;
using Fintrack.Ledger.Application;
using Fintrack.Ledger.Infrastructure;
using Fintrack.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults()
    .AddDefaultApiVersioning()
    .AddDefaultAuthentication()
    .AddDefaultOpenApi();

builder.AddApplicationServices()
    .AddInfrastructureServices()
    .AddWebServices();

var app = builder.Build();

app.MapOpenApiEndpoints()
    .MapHealthCheckEndpoints();

app.UseWebApp();

await app.RunAsync();
