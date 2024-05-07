using System.Text.Json;
using CinemaTicketing.API;
using CinemaTicketing.Contracts.Movies.Response;
using CinemaTicketing.Tests.Utils.Movies;
using FluentAssertions;

namespace CinemaTicketing.Tests.Integration.GenreController;

public class GetGenreByIdTest : IClassFixture<MovieApiFactory>
{
    private readonly HttpClient _httpClient;

    public GetGenreByIdTest(MovieApiFactory appFactory)
    {
        _httpClient = appFactory.CreateClient();
    }

    [Fact]
    public async Task GetGenreById_ReturnsSuccessStatusCode()
    {
        // Arrange
        var url = ApiEndpoints.Genres.Get.Replace("{id:int}", "1");

        // Act
        var response = await _httpClient.GetAsync(url);

        // Assert
        var responseContent = await response.Content.ReadAsStringAsync();
        var genre = JsonSerializer.Deserialize<GenreResponse>(responseContent,
            MovieConstants.GetJsonSerializerOptions());

        genre.Should().NotBeNull();
        genre!.Id.Should().Be(1);
        genre.GenreName.Should().Be("Action");
    }
}