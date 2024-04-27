using CinemaTicketing.Application.Movies.Commands;
using CinemaTicketing.Contracts.Movies;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CinemaTicketing.API.Controllers;

public class MoviesController : ApiController
{
    private readonly ISender _mediator;

    public MoviesController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost(ApiEndpoints.Movies.Create)]
    public async Task<IActionResult> Create([FromBody] CreateMovieRequest request, CancellationToken token)
    {
        var command = new CreateMovieCommand(
            request.Title,
            request.Description,
            request.YearOfRelease,
            request.Director,
            request.Duration,
            request.AgeRestriction,
            request.Genres
        );

        var result = await _mediator.Send(command, token);

        return result.Match(
            _ => CreatedAtAction(nameof(Create), new { result.Value.Id }, result.Value),
            errors => Problem(errors)
        );
    }
}