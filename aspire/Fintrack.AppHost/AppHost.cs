var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Fintrack_Ledger_API>("ledger-api")
    .WithHttpHealthCheck("/health/ready");

builder.AddProject<Projects.Fintrack_Users_API>("users-api")
    .WithHttpHealthCheck("/health/ready");

var app = builder.Build();

await app.RunAsync();
