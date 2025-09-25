var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Fintrack_Ledger_API>("ledger-api")
    .WithHttpHealthCheck("/health");

builder.AddProject<Projects.Fintrack_Users_API>("users-api")
    .WithHttpHealthCheck("/health");

builder.Build().Run();
