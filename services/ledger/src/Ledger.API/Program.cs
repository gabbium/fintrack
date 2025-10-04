using System.Reflection;
using CleanArch.AspNetCore;
using Ledger.API.BuildingBlocks.ApiVersioning.Extensions;
using Ledger.API.BuildingBlocks.HealthCheck.Extensions;
using Ledger.API.BuildingBlocks.OpenApi.Extensions;
using Ledger.API.BuildingBlocks.OpenTelemetry.Extensions;
using Ledger.API.BuildingBlocks.SerilogLogging.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddCustomSerilog();

builder.AddDefaultHealthChecks();

builder.AddDefaultOpenTelemetry();

builder.AddCustomVersioning();

builder.AddCustomOpenApi(["v1"]);

var app = builder.Build();

app.MapCustomSerilog();

app.MapDefaultHealthChecks();

app.MapGroup("/api/v{version:apiVersion}")
    .WithApiVersionSet(app.NewApiVersionSet().ReportApiVersions().Build())
    .MapApis(Assembly.GetExecutingAssembly());

app.MapCustomOpenApi();

await app.RunAsync();
