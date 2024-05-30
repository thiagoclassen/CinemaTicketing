using CinemaTicketing.API.Controllers;
using CinemaTicketing.Application.Movies.Commands;
using CinemaTicketing.Application.Movies.Mapping;
using CinemaTicketing.Application.Movies.Queries;
using CinemaTicketing.Contracts.Movies.Response;
using CinemaTicketing.Domain.Common.Errors;
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
        result.ActionName.Should().Be(nameof(_controller.GetMovieById));
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

    [Fact]
    public async Task GetMovieById_ShouldReturnMovie_WhenValidRequest()
    {
        // Arrange
        const int movieId = 10;
        var createMovieRequest = MovieConstants.GetValidMovieRequest();
        var movie = MovieConstants.GetMovie(createMovieRequest, movieId);

        _mediator.Send(Arg.Any<GetMovieByIdQuery>(),
            Arg.Any<CancellationToken>()
        )!.Returns(movie.ToErrorOr());

        // Act
        var result = (ObjectResult)await _controller.GetMovieById(movieId, CancellationToken.None);

        // Assert
        var movieResponse = (MovieResponse)result.Value!;
        result.StatusCode.Should().Be(200);
        movieResponse.Id.Should().Be(movieId);
    }

    [Fact]
    public async Task GetMovieById_ShouldReturnNoContent_WhenNoMovie()
    {
        // Arrange
        const int movieId = 10;
        var errorResponse = ErrorOrFactory.From<Movie?>(null);

        _mediator.Send(Arg.Any<GetMovieByIdQuery>(),
            Arg.Any<CancellationToken>()
        ).Returns(errorResponse);

        // Act
        var result = await _controller.GetMovieById(movieId, CancellationToken.None);

        // Assert
        result.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task ListMovies_ShouldReturnMovieList()
    {
        // Arrange
        var movies = MovieConstants.ListMovies();

        _mediator.Send(Arg.Any<ListMoviesQuery>(),
            Arg.Any<CancellationToken>()
        ).Returns(movies.ToErrorOr());

        // Act
        var result = (ObjectResult)await _controller.GetAllMovies(CancellationToken.None);

        // Assert
        result.Value.Should().BeOfType<List<MovieResponse>>();
        result.StatusCode.Should().Be(200);
        result.Value.Should().BeEquivalentTo(movies.Select(movie => movie.MapToMovieResponse()));
    }

    [Fact]
    public async Task ListMovies_ShouldReturnEmptyList_WhenNoMoviesAvailable()
    {
        // Arrange
        _mediator.Send(Arg.Any<ListMoviesQuery>(),
            Arg.Any<CancellationToken>()
        ).Returns(new List<Movie>().ToErrorOr());

        // Act
        var result = (ObjectResult)await _controller.GetAllMovies(CancellationToken.None);

        // Assert
        result.Value.Should().BeOfType<List<MovieResponse>>();
        result.StatusCode.Should().Be(200);
        ((List<MovieResponse>)result.Value!).Count.Should().Be(0);
    }

    [Fact]
    public async Task Update_ShouldReturnUpdatedMovie_WhenValidRequest()
    {
        // Arrange
        var movieId = 1;
        var movieUpdateMovieRequest = MovieConstants.GetUpdateMovieRequest();
        var movieResponse = MovieConstants.GetMovieResponse();

        _mediator.Send(Arg.Any<UpdateMovieCommand>(), Arg.Any<CancellationToken>())
            .Returns(movieResponse.ToErrorOr());

        // Act
        var result =
            (ObjectResult)await _controller.UpdateMovie(movieId, movieUpdateMovieRequest, CancellationToken.None);

        // Assert
        result.StatusCode.Should().Be(200);
        result.Value.Should().BeOfType<MovieResponse>();
    }

    [Fact]
    public async Task Update_ShouldReturnError_WhenInvalidId()
    {
        // Arrange
        var invalidId = 1;
        var movieUpdateMovieRequest = MovieConstants.GetUpdateMovieRequest();

        _mediator.Send(Arg.Any<UpdateMovieCommand>(), Arg.Any<CancellationToken>())
            .Returns(Errors.Movies.InvalidId.ToErrorOr<MovieResponse>());

        // Act
        var result =
            (ObjectResult)await _controller.UpdateMovie(invalidId, movieUpdateMovieRequest, CancellationToken.None);

        // Assert
        result.StatusCode.Should().Be(404);
        result.Value.Should().BeOfType<ProblemDetails>();
    }

    [Fact]
    public async Task Delete_ShouldReturnOk_WhenValidId()
    {
        // Arrange
        var validId = 1;

        _mediator.Send(Arg.Any<DeleteMovieCommand>(), Arg.Any<CancellationToken>())
            .Returns(ErrorOrFactory.From(1));

        // Act
        var result = (ObjectResult)await _controller.DeleteMovieById(validId, CancellationToken.None);

        // Assert
        result.StatusCode.Should().Be(200);
        result.Value.Should().Be(1);
    }

    [Fact]
    public async Task Delete_ShouldReturnError_WhenInvalidId()
    {
        // Arrange
        var invalidId = 999;

        _mediator.Send(Arg.Any<DeleteMovieCommand>(), Arg.Any<CancellationToken>())
            .Returns(Errors.Movies.InvalidId.ToErrorOr<int>());

        // Act
        var result = (ObjectResult)await _controller.DeleteMovieById(invalidId, CancellationToken.None);

        // Assert
        result.StatusCode.Should().Be(404);
        result.Value.Should().BeOfType<ProblemDetails>();
    }
}