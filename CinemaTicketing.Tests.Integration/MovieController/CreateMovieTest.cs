using System.Net;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using CinemaTicketing.API;
using CinemaTicketing.Domain.Movies;
using CinemaTicketing.Tests.Utils;
using FluentAssertions;

namespace CinemaTicketing.Tests.Integration.MovieController;

public class CreateMovieTest : IClassFixture<MovieApiFactory>
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
        var movieRequest = MoviesUtils.GetMovieRequest();
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
            .Deserialize<Movie>(
                contentResponse,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

        result.StatusCode.Should().Be(HttpStatusCode.Created);
        movieResponse.Should().BeOfType<Movie>();
        movieResponse!.Id.Should().BeGreaterThan(0);
    }
}