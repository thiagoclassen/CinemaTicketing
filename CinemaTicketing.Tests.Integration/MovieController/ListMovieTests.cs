using System.Net;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using CinemaTicketing.API;
using CinemaTicketing.Contracts.Movies.Response;
using CinemaTicketing.Tests.Utils.Movies;
using FluentAssertions;

namespace CinemaTicketing.Tests.Integration.MovieController;

[Collection(nameof(IntegrationTestsCollection))]
public class ListMovieTests
{
    private readonly HttpClient _httpClient;

    public ListMovieTests(MovieApiFactory appFactory)
    {
        _httpClient = appFactory.CreateClient();
    }

    [Fact]
    public async Task List_ShouldReturnMovies_WhenMoviesExist()
    {
        // Arrange
        var movieRequest = MovieConstants.GetValidMovieRequest();
        var content = new StringContent(
            JsonSerializer.Serialize(movieRequest),
            Encoding.UTF8,
            MediaTypeNames.Application.Json);
        await _httpClient.PostAsync(ApiEndpoints.Movies.Create, content);

        // Act
        var getResult = await _httpClient
            .GetAsync(ApiEndpoints.Movies.GetAll);

        // Assert
        var contentGetResponse = await getResult.Content.ReadAsStringAsync();

        var movieGetResponse = JsonSerializer
            .Deserialize<List<MovieResponse>>(
                contentGetResponse,
                MovieConstants.GetJsonSerializerOptions()
            );

        getResult.StatusCode.Should().Be(HttpStatusCode.OK);
        movieGetResponse.Should().BeOfType<List<MovieResponse>>();
        movieGetResponse!.Count.Should().BeGreaterThan(0);
    }
}