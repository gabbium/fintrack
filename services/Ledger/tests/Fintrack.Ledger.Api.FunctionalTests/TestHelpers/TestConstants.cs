namespace Fintrack.Ledger.Api.FunctionalTests.TestHelpers;

public static class TestConstants
{
    public static readonly JsonSerializerOptions Json = new(JsonSerializerDefaults.Web)
    {
        Converters = { new JsonStringEnumConverter() }
    };
}
