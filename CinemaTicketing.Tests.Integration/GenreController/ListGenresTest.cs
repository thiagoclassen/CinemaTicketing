using System.Text.Json;
using CinemaTicketing.API;
using CinemaTicketing.Contracts.Movies.Response;
using CinemaTicketing.Tests.Utils.Movies;
using FluentAssertions;

namespace CinemaTicketing.Tests.Integration.GenreController;

public class ListGenresTest : IClassFixture<MovieApiFactory>
{
    private readonly HttpClient _httpClient;

    public ListGenresTest(MovieApiFactory appFactory)
    {
        _httpClient = appFactory.CreateClient();
    }

    [Fact]
    public async Task ListGenres_ReturnsSuccessStatusCode()
    {
        // Act
        var response = await _httpClient.GetAsync(ApiEndpoints.Genres.GetAll);

        // Assert
        var responseContent = await response.Content.ReadAsStringAsync();
        var genresList =
            JsonSerializer.Deserialize<List<GenreResponse>>(responseContent, MovieConstants.GetJsonSerializerOptions());

        genresList.Should().NotBeNullOrEmpty();
        genresList!.Count.Should().BeGreaterThan(0);
        genresList.Should().BeOfType<List<GenreResponse>>();
    }
}