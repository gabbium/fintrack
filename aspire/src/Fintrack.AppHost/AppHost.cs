var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres");

var postgresdb = postgres.AddDatabase("postgresdb");

var ledgerMigrations = builder.AddProject<Projects.Fintrack_Ledger_MigrationService>("ledgermigrations")
    .WithReference(postgresdb).WaitFor(postgresdb);

builder.AddProject<Projects.Fintrack_Ledger_API>("ledgerapi")
    .WithReference(postgresdb).WaitFor(postgresdb)
    .WithReference(ledgerMigrations).WaitForCompletion(ledgerMigrations)
    .WithHttpHealthCheck("/health/ready");

builder.AddProject<Projects.Fintrack_Users_API>("usersapi")
    .WithHttpHealthCheck("/health/ready");

var app = builder.Build();

await app.RunAsync();
