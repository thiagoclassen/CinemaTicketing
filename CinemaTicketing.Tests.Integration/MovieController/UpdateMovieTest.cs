using System.Net;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using CinemaTicketing.API;
using CinemaTicketing.Contracts.Movies.Response;
using CinemaTicketing.Tests.Utils.Movies;
using FluentAssertions;

namespace CinemaTicketing.Tests.Integration.MovieController;

public class UpdateMovieTest : IClassFixture<MovieApiFactory>
{
    private readonly HttpClient _httpClient;

    public UpdateMovieTest(MovieApiFactory appFactory)
    {
        _httpClient = appFactory.CreateClient();
    }

    [Fact]
    public async Task Update_ShouldUpdateMovie_WhenRequestIsValid()
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

        var updateMovieRequest = MovieConstants.GetUpdateMovieRequest();

        var updateContent = new StringContent(
            JsonSerializer.Serialize(updateMovieRequest),
            Encoding.UTF8,
            MediaTypeNames.Application.Json);

        // Act
        var updateResult = await _httpClient
            .PutAsync(UpdateMovieUrl(movieResponse!.Id), updateContent);

        // Assert
        var contentUpdateResponse = await updateResult.Content.ReadAsStringAsync();
        var movieUpdateResponse = JsonSerializer
            .Deserialize<MovieResponse>(
                contentUpdateResponse,
                MovieConstants.GetJsonSerializerOptions()
            );

        updateResult.StatusCode.Should().Be(HttpStatusCode.OK);
        movieUpdateResponse.Should().BeOfType<MovieResponse>();
        movieUpdateResponse!.Id.Should().Be(movieResponse!.Id);
        movieUpdateResponse!.Title.Should().Be(updateMovieRequest.Title);
    }

    [Fact]
    public async Task Update_ShouldNotUpdateMovie_WhenRequestIsInvalid()
    {
        // Arrange
        var updateMovieRequest = MovieConstants.GetUpdateMovieRequest();

        var updateContent = new StringContent(
            JsonSerializer.Serialize(updateMovieRequest),
            Encoding.UTF8,
            MediaTypeNames.Application.Json);

        // Act
        var updateResult = await _httpClient
            .PutAsync(UpdateMovieUrl(9999), updateContent);

        // Assert
        var contentUpdateResponse = await updateResult.Content.ReadAsStringAsync();

        var jsonDocument = JsonDocument.Parse(contentUpdateResponse).RootElement;


        updateResult.StatusCode.Should().Be(HttpStatusCode.NotFound);
        jsonDocument.GetProperty("title").GetString().Should().Be("Not Found");
        jsonDocument.GetProperty("detail").GetString().Should().Be("Invalid Movie Id.");
        jsonDocument.GetProperty("errorCodes")[0].GetString().Should().Be("Movie.InvalidId");
    }

    private static string UpdateMovieUrl(int id)
    {
        return ApiEndpoints.Movies
            .Update
            .Replace("{id:int}", id.ToString());
    }
}