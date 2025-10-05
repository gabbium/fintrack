var builder = DistributedApplication.CreateBuilder(args);

var keycloak = builder.AddKeycloak("keycloak", 8080)
    .WithDataVolume()
    .WithRealmImport("./Realms")
    .WithLifetime(ContainerLifetime.Persistent);

var postgres = builder.AddPostgres("postgres")
    .WithDataVolume()
    .WithHostPort(5432)
    .WithLifetime(ContainerLifetime.Persistent);

var ledgerDb = postgres.AddDatabase("ledgerdb");

// Services
builder.AddProject<Projects.Fintrack_Ledger_MigrationService>("fintrack-ledger-migrationservice")
    .WithReference(ledgerDb).WaitFor(ledgerDb);

builder.AddProject<Projects.Fintrack_Ledger_Api>("fintrack-ledger-api")
    .WithReference(keycloak).WaitFor(keycloak)
    .WithReference(ledgerDb).WaitFor(ledgerDb)
    .WithEnvironment(ctx =>
    {
        ctx.EnvironmentVariables["Authentication__OidcJwt__Authority"] = $"http:/localhost:8080/realms/fintrack";
    });

var app = builder.Build();

await app.RunAsync();
