namespace Fintrack.Planning.Api.FunctionalTests.TestSupport;

[CollectionDefinition(Name)]
public class TestsCollection : ICollectionFixture<TestsFixture>
{
    public const string Name = "FunctionalTests";
}
