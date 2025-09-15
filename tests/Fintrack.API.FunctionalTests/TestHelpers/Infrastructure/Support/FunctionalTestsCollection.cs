namespace Fintrack.API.FunctionalTests.TestHelpers.Infrastructure.Support;

[CollectionDefinition(Name)]
public sealed class FunctionalTestsCollection : ICollectionFixture<TestFixture>
{
    public const string Name = "FunctionalTests";
}
