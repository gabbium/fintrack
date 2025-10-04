using Serilog;

namespace Ledger.API.BuildingBlocks.SerilogLogging.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication MapCustomSerilog(this WebApplication app)
    {
        app.UseSerilogRequestLogging(options =>
        {
            options.IncludeQueryInRequestPath = true;
        });

        return app;
    }
}
