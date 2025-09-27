using Fintrack.ServiceDefaults;
using Fintrack.Users.API;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults()
    .AddApiVersioningDefaults()
    .AddOpenApiDefaults();

builder.AddWebServices();

var app = builder.Build();

app.MapOpenApiEndpoints()
    .MapHealthCheckEndpoints()
    .UseWebApp();

await app.RunAsync();
