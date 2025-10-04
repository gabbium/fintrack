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
builder.AddProject<Projects.Fintrack_Ledger_Worker_Maintenance>("fintrack-ledger-worker-maintenance")
    .WithReference(ledgerDb).WaitFor(ledgerDb);

builder.AddProject<Projects.Fintrack_Ledger_Api>("fintrack-ledger-api")
    .WithReference(keycloak).WaitFor(keycloak)
    .WithReference(ledgerDb).WaitFor(ledgerDb)
    .WithEnvironment(ctx =>
    {
        var baseUrl = keycloak.GetEndpoint("http").Url;
        ctx.EnvironmentVariables["Authentication__OidcJwt__Authority"] = $"{baseUrl}/realms/fintrack";
    });

var app = builder.Build();

await app.RunAsync();
