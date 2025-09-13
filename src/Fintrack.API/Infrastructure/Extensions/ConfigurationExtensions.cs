namespace Fintrack.API.Infrastructure.Extensions;

public static class ConfigurationExtensions
{
    public static string GetRequiredValue(this IConfiguration configuration, string name)
    {
        var value = configuration[name];

        return value is null
            ? throw new InvalidOperationException($"Configuration missing value for: {(configuration is IConfigurationSection s ? s.Path + ":" + name : name)}")
            : value;
    }
}
