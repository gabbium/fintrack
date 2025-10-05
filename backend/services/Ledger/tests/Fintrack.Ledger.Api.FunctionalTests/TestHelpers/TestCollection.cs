namespace Fintrack.Ledger.Api.FunctionalTests.TestHelpers;

[CollectionDefinition(Name)]
public class TestCollection : ICollectionFixture<TestFixture>
{
    public const string Name = "FunctionalTests";
}
