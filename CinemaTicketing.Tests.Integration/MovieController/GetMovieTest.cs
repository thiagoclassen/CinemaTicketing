using System.Net;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using CinemaTicketing.API;
using CinemaTicketing.Contracts.Movies.Response;
using CinemaTicketing.Tests.Utils.Movies;
using FluentAssertions;

namespace CinemaTicketing.Tests.Integration.MovieController;

public class GetMovieTest : IClassFixture<MovieApiFactory>
{
    private readonly HttpClient _httpClient;

    public GetMovieTest(MovieApiFactory appFactory)
    {
        _httpClient = appFactory.CreateClient();
    }

    [Fact]
    public async Task GetMovieById_ShouldReturnMovie_WhenMovieExists()
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
        var getResult = await _httpClient
            .GetAsync(url);

        // Assert
        var contentGetResponse = await getResult.Content.ReadAsStringAsync();

        var movieGetResponse = JsonSerializer
            .Deserialize<MovieResponse>(
                contentGetResponse,
                MovieConstants.GetJsonSerializerOptions()
            );

        getResult.StatusCode.Should().Be(HttpStatusCode.OK);
        movieGetResponse.Should().BeOfType<MovieResponse>();
        movieGetResponse!.Id.Should().Be(movieResponse.Id);
    }

    [Fact]
    public async Task GetMovieById_ShouldReturnNoContent_WhenMovieDoesNotExist()
    {
        // Arrange
        var url = GetMovieUrl(999);

        // Act
        var getResult = await _httpClient
            .GetAsync(url);

        // Assert
        getResult.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    private static string GetMovieUrl(int id)
    {
        return ApiEndpoints.Movies
            .Get
            .Replace("{id:int}", id.ToString());
    }
}