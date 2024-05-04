using CinemaTicketing.Contracts.Movies.Request;
using CinemaTicketing.Contracts.Movies.Response;
using FluentAssertions;

namespace CinemaTicketing.Tests.Utils.Movies;

public static class MovieExtensionValidators
{
    public static void ValidateCreation(this MovieResponse movieResponse, CreateMovieRequest createMovieRequest)
    {
        movieResponse.Id.Should().BeGreaterThan(0);
        movieResponse.Description.Should().Be(createMovieRequest.Description);
        movieResponse.Title.Should().Be(createMovieRequest.Title);
        movieResponse.Director.Should().Be(createMovieRequest.Director);
        movieResponse.Duration.Should().Be(createMovieRequest.Duration);
        movieResponse.AgeRestriction.Should().Be(createMovieRequest.AgeRestriction);
        movieResponse.YearOfRelease.Should().Be(createMovieRequest.YearOfRelease);
        movieResponse.Genres.Should().HaveSameCount(createMovieRequest.Genres);
        movieResponse
            .Genres
            .Zip(createMovieRequest.Genres)
            .ToList()
            .ForEach(pair => pair.First.Should().BeSameAs(pair.Second.GenreName));
    }
}