var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithDataVolume()
    .WithHostPort(5432);

var postgresDb = postgres.AddDatabase("postgresdb");

var keycloak = builder.AddKeycloak("keycloak", 8080)
    .WithDataVolume();

var ledgerMigrator = builder.AddProject<Projects.Fintrack_Ledger_MigrationService>("ledger-migrator")
    .WithReference(postgresDb).WaitFor(postgresDb);

var ledgerApi = builder.AddProject<Projects.Fintrack_Ledger_API>("ledger-api")
    .WithReference(postgresDb).WaitFor(postgresDb)
    .WithReference(keycloak).WaitFor(keycloak)
    .WithReference(ledgerMigrator).WaitForCompletion(ledgerMigrator)
    .WithHttpHealthCheck("/health/ready");

var app = builder.Build();

await app.RunAsync();
