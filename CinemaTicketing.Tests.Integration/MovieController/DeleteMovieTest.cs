using System.Net;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using CinemaTicketing.API;
using CinemaTicketing.Contracts.Movies.Response;
using CinemaTicketing.Tests.Utils.Movies;
using FluentAssertions;

namespace CinemaTicketing.Tests.Integration.MovieController;

public class DeleteMovieTest : IClassFixture<MovieApiFactory>
{
    private readonly HttpClient _httpClient;

    public DeleteMovieTest(MovieApiFactory appFactory)
    {
        _httpClient = appFactory.CreateClient();
    }

    [Fact]
    public async Task Delete_ShouldDeleteMovie_WhenMovieExists()
    {
        // Arrange
        var movieRequest = MovieConstants.GetValidMovieRequest();
        var content = new StringContent(
            JsonSerializer.Serialize(movieRequest),
            Encoding.UTF8,
            MediaTypeNames.Application.Json);
        var createResult = await _httpClient
            .PostAsync(ApiEndpoints.Movies.Create, content);
        var contentResponse = await createResult.Content.ReadAsStringAsync();
        var movieResponse = JsonSerializer
            .Deserialize<MovieResponse>(
                contentResponse,
                MovieConstants.GetJsonSerializerOptions()
            );

        var url = GetMovieUrl(movieResponse!.Id);

        // Act
        var deleteResult = await _httpClient
            .DeleteAsync(url);

        // Assert
        deleteResult.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Delete_ShouldReturnNoContent_WhenMovieDoesNotExist()
    {
        // Arrange
        var url = GetMovieUrl(999);

        // Act
        var deleteResult = await _httpClient
            .DeleteAsync(url);

        // Assert
        deleteResult.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    private static string GetMovieUrl(int id)
    {
        return ApiEndpoints.Movies
            .Delete
            .Replace("{id:int}", id.ToString());
    }
}