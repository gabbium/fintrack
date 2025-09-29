var builder = DistributedApplication.CreateBuilder(args);

var keycloak = builder.AddKeycloak("keycloak", 8080)
    .WithDataVolume()
    .WithRealmImport("./Realms");

var postgres = builder.AddPostgres("postgres")
    .WithDataVolume()
    .WithHostPort(5432);

var ledgerDb = postgres.AddDatabase("ledgerdb");

var ledgerMigrator = builder.AddProject<Projects.Fintrack_Ledger_MigrationService>("ledger-migrator")
    .WithReference(ledgerDb).WaitFor(ledgerDb);

builder.AddProject<Projects.Fintrack_Ledger_API>("ledger-api")
    .WithReference(keycloak).WaitFor(keycloak)
    .WithReference(ledgerDb).WaitFor(ledgerDb)
    .WithReference(ledgerMigrator).WaitForCompletion(ledgerMigrator)
    .WithEnvironment("Identity__Url", "http://localhost:8080/realms/fintrack")
    .WithHttpHealthCheck("/health/ready");

var app = builder.Build();

await app.RunAsync();
