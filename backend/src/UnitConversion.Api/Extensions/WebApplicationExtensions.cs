namespace UnitConversion.Api.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication UseHealthEndpoints(this WebApplication app)
    {
        app.MapHealthChecks("/health");
        return app;
    }
}
