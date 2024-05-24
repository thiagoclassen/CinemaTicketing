using CinemaTicketing.Application.Movies.Commands;
using CinemaTicketing.Application.Movies.Repositories;
using CinemaTicketing.Domain.Movies;
using CinemaTicketing.Tests.Utils.Movies;
using FluentAssertions;
using NSubstitute;

namespace CinemaTicketing.Application.Tests.Unit.Movies.Commands;

public class CreateMovieCommandTest
{
    private readonly CreateMovieCommandHandler _handler;
    private readonly IMovieRepository _movieRepository = Substitute.For<IMovieRepository>();

    public CreateMovieCommandTest()
    {
        _handler = new CreateMovieCommandHandler(_movieRepository);
    }

    [Fact]
    public async Task Handle_ShouldReturnMovie_WhenValid()
    {
        // Arrange
        var createMovieCommand = MovieConstants.GetMovieCommand();
        var movie = MovieConstants.GetMovie(createMovieCommand);
        _movieRepository.AddAsync(movie, Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(createMovieCommand, CancellationToken.None);

        // Assert
        result.IsError.Should().BeFalse();
        result.Value.Should().BeEquivalentTo(movie);
#pragma warning disable CS4014
        _movieRepository.Received(1).AddAsync(Arg.Any<Movie>(), Arg.Any<CancellationToken>());
#pragma warning restore CS4014
    }
}