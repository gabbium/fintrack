using Scalar.AspNetCore;

namespace Ledger.API.BuildingBlocks.OpenApi.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication MapCustomOpenApi(this WebApplication app)
    {
        app.MapOpenApi();

        app.MapScalarApiReference(scalarOptions =>
        {
            scalarOptions.Theme = ScalarTheme.Mars;
            scalarOptions.DefaultFonts = false;
        });

        app.MapGet("/", () => Results.Redirect("/scalar/v1")).ExcludeFromDescription();

        return app;
    }
}
