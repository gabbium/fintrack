namespace Fintrack.Planning.Api.FunctionalTests.TestHelpers;

[CollectionDefinition(Name)]
public class FunctionalTestsCollection : ICollectionFixture<FunctionalTestsFixture>
{
    public const string Name = "FunctionalTests";
}
