using System.Reflection;
using CleanArch.AspNetCore;
using Ledger.API.BuildingBlocks.ApiVersioning.Extensions;
using Ledger.API.BuildingBlocks.OpenApi.Extensions;
using Ledger.API.BuildingBlocks.OpenTelemetry.Extensions;

var builder = WebApplication.CreateBuilder(args);

//builder.AddCustomSerilog();

builder.AddCustomOpenTelemetry();

builder.AddCustomVersioning();

builder.AddCustomOpenApi(["v1"]);

var app = builder.Build();

var versionSet = app.NewApiVersionSet()
    .ReportApiVersions()
    .Build();

app.MapGroup("/api/v{version:apiVersion}")
    .WithApiVersionSet(versionSet)
    .MapApis(Assembly.GetExecutingAssembly());

app.MapCustomOpenApi();

await app.RunAsync();
