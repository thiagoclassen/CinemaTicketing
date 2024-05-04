namespace CinemaTicketing.Tests.Integration;

[CollectionDefinition(nameof(IntegrationTestsCollection))]
public class IntegrationTestsCollection : IClassFixture<MovieApiFactory>
{
}