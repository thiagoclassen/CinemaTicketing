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
public class CreateMovieTest
{
    private readonly HttpClient _httpClient;

    public CreateMovieTest(MovieApiFactory appFactory)
    {
        _httpClient = appFactory.CreateClient();
    }

    [Fact]
    public async Task Create_ShouldAddMovie_WhenRequestIsValid()
    {
        // Arrange
        var movieRequest = MovieConstants.GetValidMovieRequest();
        var content = new StringContent(
            JsonSerializer.Serialize(movieRequest),
            Encoding.UTF8,
            MediaTypeNames.Application.Json);
        // Act
        var result = await _httpClient
            .PostAsync(ApiEndpoints.Movies.Create, content);

        // Assert
        var contentResponse = await result.Content.ReadAsStringAsync();
        var movieResponse = JsonSerializer
            .Deserialize<MovieResponse>(
                contentResponse,
                MovieConstants.GetJsonSerializerOptions()
            );

        result.StatusCode.Should().Be(HttpStatusCode.Created);
        movieResponse.Should().BeOfType<MovieResponse>();
        movieResponse!.Id.Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task Create_ShouldReturnError_WhenRequestIsInvalid()
    {
        // Arrange
        var movieRequest = MovieConstants.GetInvalidMovieRequest();
        var content = new StringContent(
            JsonSerializer.Serialize(movieRequest),
            Encoding.UTF8,
            MediaTypeNames.Application.Json);
        // Act
        var result = await _httpClient
            .PostAsync(ApiEndpoints.Movies.Create, content);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}