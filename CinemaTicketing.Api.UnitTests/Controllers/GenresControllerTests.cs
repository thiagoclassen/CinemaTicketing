using CinemaTicketing.API.Controllers;
using CinemaTicketing.Application.Movies.Queries;
using CinemaTicketing.Contracts.Movies.Response;
using CinemaTicketing.Domain.Movies;
using ErrorOr;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace CinemaTicketing.Api.Tests.Unit.Controllers;

public class GenresControllerTests
{
    private readonly GenresController _controller;
    private readonly ISender _mediator = Substitute.For<ISender>();

    public GenresControllerTests()
    {
        _controller = new GenresController(_mediator);
    }

    [Fact]
    public async Task ListGenres_ShouldReturnGenreList()
    {
        // Arrange
        var genres = Genre.ListGenres().ToList();

        _mediator.Send(Arg.Any<ListGenresQuery>(), Arg.Any<CancellationToken>())
            .Returns(genres.ToErrorOr());

        // Act
        var result = (ObjectResult)await _controller.ListGenres(CancellationToken.None);

        // Assert
        result.StatusCode.Should().Be(200);
        ((List<GenreResponse>)result.Value!).Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task ListGenres_ShouldReturnEmptyList()
    {
        // Arrange
        var genres = new List<Genre>();

        _mediator.Send(Arg.Any<ListGenresQuery>(), Arg.Any<CancellationToken>())
            .Returns(genres.ToErrorOr());

        // Act
        var result = (ObjectResult)await _controller.ListGenres(CancellationToken.None);

        // Assert
        result.StatusCode.Should().Be(200);
        ((List<GenreResponse>)result.Value!).Count.Should().Be(0);
    }

    [Fact]
    public async Task GetGenreById_ShouldReturnGenre_WhenValidId()
    {
        // Arrange
        const int genreId = 1;
        var genre = Genre.ListGenres().First(g => g.Id == genreId);

        _mediator.Send(Arg.Any<GetGenreByIdQuery>(), Arg.Any<CancellationToken>())!
            .Returns(genre.ToErrorOr());

        // Act
        var result = (ObjectResult)await _controller.GetGenreById(genreId, CancellationToken.None);

        // Assert
        result.StatusCode.Should().Be(200);
        ((GenreResponse)result.Value!).Id.Should().Be(genreId);
    }

    [Fact]
    public async Task GetGenreById_ShouldReturnError_WheninvalidId()
    {
        // Arrange
        const int invalidGenreId = 999;

        _mediator.Send(Arg.Any<GetGenreByIdQuery>(), Arg.Any<CancellationToken>())
            .Returns(ErrorOrFactory.From<Genre?>(null));

        // Act
        var result = (NoContentResult)await _controller.GetGenreById(invalidGenreId, CancellationToken.None);

        // Assert
        result.StatusCode.Should().Be(204);
    }
}