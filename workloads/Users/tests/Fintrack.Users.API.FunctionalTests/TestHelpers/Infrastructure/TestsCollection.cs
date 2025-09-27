namespace Fintrack.Users.API.FunctionalTests.TestHelpers.Infrastructure;

[CollectionDefinition(Name)]
public class TestsCollection : ICollectionFixture<TestFixture>
{
    public const string Name = "FunctionalTests";
}
