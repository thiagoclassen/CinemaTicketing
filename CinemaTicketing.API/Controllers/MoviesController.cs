using CinemaTicketing.Application.Movies.Commands;
using CinemaTicketing.Application.Movies.Mapping;
using CinemaTicketing.Application.Movies.Queries;
using CinemaTicketing.Contracts.Movies.Request;
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
        var command = request.MapToCreateMovieCommand();

        var result = await _mediator.Send(command, token);

        return result.Match(
            _ => CreatedAtAction(nameof(Create), new { result.Value.Id }, result.Value.MapToMovieResponse()),
            errors => Problem(errors)
        );
    }

    [HttpGet(ApiEndpoints.Movies.Get)]
    public async Task<IActionResult> GetMovieById([FromRoute] int id, CancellationToken token)
    {
        var command = new GetMovieByIdQuery(id);

        var result = await _mediator.Send(command, token);

        return result.Match(
            _ => result.Value is null ? NoContent() : Ok(result.Value.MapToMovieResponse()),
            errors => Problem(errors));
    }

    [HttpGet(ApiEndpoints.Movies.GetAll)]
    public async Task<IActionResult> GetAllMovies(CancellationToken token)
    {
        var command = new ListMoviesQuery();

        var result = await _mediator.Send(command, token);

        return result.Match(
            _ => Ok(result.Value.MapToListMovieResponse()),
            errors => Problem(errors)
        );
    }

    [HttpDelete(ApiEndpoints.Movies.Delete)]
    public async Task<IActionResult> DeleteMovieById([FromRoute] int id, CancellationToken token)
    {
        var command = new DeleteMovieCommand(id);

        var result = await _mediator.Send(command, token);

        return result.Match(
            _ => Ok(result.Value),
            errors => Problem(errors)
        );
    }

    [HttpPut(ApiEndpoints.Movies.Update)]
    public async Task<IActionResult> UpdateMovie([FromRoute] int id, [FromBody] UpdateMovieRequest updateMovieRequest,
        CancellationToken token)
    {
        var command = updateMovieRequest.MapToUpdateMovieCommand(id);

        var result = await _mediator.Send(command, token);

        return result.Match(
            _ => Ok(result.Value),
            errors => Problem(errors)
        );
    }
}