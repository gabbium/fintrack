var builder = DistributedApplication.CreateBuilder(args);

var keycloak = builder.AddKeycloak("keycloak", 8080)
    .WithDataVolume()
    .WithRealmImport("../../../infra/keycloak/realms")
    .WithLifetime(ContainerLifetime.Persistent);

var postgres = builder.AddPostgres("postgres")
    .WithDataVolume()
    .WithHostPort(5432)
    .WithLifetime(ContainerLifetime.Persistent);

var ledgerDb = postgres.AddDatabase("ledgerdb");
var planningDb = postgres.AddDatabase("planningdb");

// Services
builder.AddProject<Projects.Fintrack_Ledger_MigrationService>("ledger-migrationservice")
    .WithReference(ledgerDb).WaitFor(ledgerDb);

builder.AddProject<Projects.Fintrack_Ledger_Api>("ledger-api")
    .WithReference(keycloak).WaitFor(keycloak)
    .WithReference(ledgerDb).WaitFor(ledgerDb)
    .WithEnvironment(ctx =>
    {
        var keycloakUrl = keycloak.GetEndpoint("http").Url;
        ctx.EnvironmentVariables["Authentication__OidcJwt__Authority"] = $"{keycloakUrl}/realms/fintrack";
    });

builder.AddProject<Projects.Fintrack_Planning_MigrationService>("planning-migrationservice")
    .WithReference(planningDb).WaitFor(planningDb);

builder.AddProject<Projects.Fintrack_Planning_Api>("planning-api")
    .WithReference(keycloak).WaitFor(keycloak)
    .WithReference(planningDb).WaitFor(planningDb)
    .WithEnvironment(ctx =>
    {
        var keycloakUrl = keycloak.GetEndpoint("http").Url;
        ctx.EnvironmentVariables["Authentication__OidcJwt__Authority"] = $"{keycloakUrl}/realms/fintrack";
    });

builder.AddProject<Projects.Fintrack_Planning_Publisher>("planning-publisher")
    .WithReference(planningDb).WaitFor(planningDb);

var app = builder.Build();

await app.RunAsync();
