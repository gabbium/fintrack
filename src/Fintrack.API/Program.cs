using Fintrack.API;
using Fintrack.API.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddDefaultLogging();
builder.AddDefaultHealthChecks();
builder.AddDefaultAuthentication();
builder.AddDefaultApiVersioning();
builder.AddDefaultOpenApi();

builder.AddIdentityServices();
builder.AddLedgerServices();
builder.AddWebServices();

var app = builder.Build();

app.MapOpenApiEndpoints();

app.MapHealthEndpoints();

app.UseWebApp();

await app.RunAsync();

public partial class Program
{
    protected Program()
    {
    }
}
