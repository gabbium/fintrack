namespace Fintrack.Ledger.API.FunctionalTests.TestHelpers.Infrastructure.Support;

[CollectionDefinition(Name)]
public class TestsCollection : ICollectionFixture<TestFixture>
{
    public const string Name = "FunctionalTests";
}
