namespace Fintrack.API.Infrastructure.Extensions;

public static class ConfigurationExtensions
{
    public static string GetRequiredValue(this IConfiguration configuration, string name)
    {
        var value = configuration[name];

        if (value is not null)
        {
            return value;
        }

        var key = configuration is IConfigurationSection section
            ? $"{section.Path}:{name}"
            : name;

        throw new InvalidOperationException($"Configuration missing value for: {key}");
    }
}
