using CinemaTicketing.API.Controllers;
using CinemaTicketing.Application.Movies.Commands;
using CinemaTicketing.Contracts.Movies.Response;
using CinemaTicketing.Domain.Movies;
using CinemaTicketing.Tests.Utils.Movies;
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
        var createMovieRequest = MovieConstants.GetValidMovieRequest();
        var movie = MovieConstants.GetMovie(createMovieRequest);

        _mediator.Send(Arg.Any<CreateMovieCommand>(), Arg.Any<CancellationToken>())
            .Returns(movie.ToErrorOr());

        // Act
        var result = (CreatedAtActionResult)await _controller.Create(createMovieRequest, CancellationToken.None);

        // Assert
        var movieResponse = (MovieResponse)result.Value!;
        result.StatusCode.Should().Be(201);
        movieResponse.ValidateCreation(createMovieRequest);
        result.ActionName.Should().Be(nameof(_controller.Create));
    }

    [Fact]
    public async Task Create_ShouldReturnError_WhenInvalidRequest()
    {
        // Arrange
        var createMovieRequest = MovieConstants.GetValidMovieRequest();

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