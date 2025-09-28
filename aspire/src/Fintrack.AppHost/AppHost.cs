var builder = DistributedApplication.CreateBuilder(args);

var keycloak = builder.AddKeycloak("keycloak", 8080)
    .WithDataVolume();

var postgres = builder.AddPostgres("postgres")
    .WithDataVolume()
    .WithHostPort(5432);

var ledgerDb = postgres.AddDatabase("ledgerdb");

var ledgerMigrator = builder.AddProject<Projects.Fintrack_Ledger_MigrationService>("ledger-migrator")
    .WithReference(ledgerDb).WaitFor(ledgerDb);

var ledgerApi = builder.AddProject<Projects.Fintrack_Ledger_API>("ledger-api")
    .WithReference(keycloak).WaitFor(keycloak)
    .WithReference(ledgerDb).WaitFor(ledgerDb)
    .WithReference(ledgerMigrator).WaitForCompletion(ledgerMigrator)
    .WithHttpHealthCheck("/health/ready");

var app = builder.Build();

await app.RunAsync();
