using CinemaTicketing.Application.Common.Interfaces;
using CinemaTicketing.Application.Movies.Commands;
using CinemaTicketing.Domain.Movies;
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
        var createMovieCommand = new CreateMovieCommand(
            "The Matrix",
            "A computer hacker learns from mysterious rebels about the true nature of his reality and his role in the war against its controllers.",
            1999,
            "Lana Wachowski, Lilly Wachowski",
            120,
            15,
            new List<Genre>
            {
                new() { GenreName = "Action" },
                new() { GenreName = "Sci-Fi" }
            }
        );
        var movie = new Movie
        {
            Id = 0,
            Title = createMovieCommand.Title,
            Description = createMovieCommand.Description,
            YearOfRelease = createMovieCommand.YearOfRelease,
            Director = createMovieCommand.Director,
            Duration = createMovieCommand.Duration,
            AgeRestriction = createMovieCommand.AgeRestriction,
            Genres = createMovieCommand.Genres
        };
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