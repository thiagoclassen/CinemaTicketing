using CinemaTicketing.API.Controllers;
using CinemaTicketing.Application.Movies.Commands;
using CinemaTicketing.Domain.Movies;
using CinemaTicketing.Tests.Utils;
using ErrorOr;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace CinemaTicketing.Api.Tests.Unit.Controllers;

public class MoviesControllerTests
{
    private readonly MoviesController _controller;
    private readonly ISender _mediator = Substitute.For<ISender>();

    public MoviesControllerTests()
    {
        _controller = new MoviesController(_mediator);
    }

    [Fact]
    public async Task Create_ShouldReturnCreatedAtAction_WhenValidRequest()
    {
        // Arrange
        var createMovieRequest = MoviesUtils.GetMovieRequest();
        var movie = MoviesUtils.GetMovie(createMovieRequest);

        _mediator.Send(Arg.Any<CreateMovieCommand>(), Arg.Any<CancellationToken>())
            .Returns(movie.ToErrorOr());

        // Act
        var result = (CreatedAtActionResult)await _controller.Create(createMovieRequest, CancellationToken.None);

        // Assert
        result.StatusCode.Should().Be(201);
        result.Value.Should().BeEquivalentTo(movie);
        result.ActionName.Should().Be(nameof(_controller.Create));
    }

    [Fact]
    public async Task Create_ShouldReturnError_WhenInvalidRequest()
    {
        // Arrange
        var createMovieRequest = MoviesUtils.GetMovieRequest();

        _mediator.Send(Arg.Any<CreateMovieCommand>(),
            Arg.Any<CancellationToken>()
        ).Returns(Error.Validation().ToErrorOr<Movie>());

        // Act
        var result = (ObjectResult)await _controller.Create(createMovieRequest, CancellationToken.None);

        // Assert
        result.Value.Should().BeOfType<ValidationProblemDetails>();
        result.Value.As<ValidationProblemDetails>().Errors.Should().NotBeEmpty();
    }
}