using CinemaTicketing.Application.Movies.Mapping;
using CinemaTicketing.Application.Movies.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CinemaTicketing.API.Controllers;

public class GenresController : ApiController
{
    private readonly ISender _mediator;

    public GenresController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet(ApiEndpoints.Genres.Get)]
    public async Task<IActionResult> GetGenreById(int id, CancellationToken cancellationToken)
    {
        var command = new GetGenreByIdQuery(id);
        var result = await _mediator.Send(command, cancellationToken);

        return result.Match(
            _ => result.Value is null ? NoContent() : Ok(result.Value.MapToGenreResponse()),
            errors => Problem(errors));
    }

    [HttpGet(ApiEndpoints.Genres.GetAll)]
    public async Task<IActionResult> ListGenres(CancellationToken cancellationToken)
    {
        var command = new ListGenresQuery();
        var result = await _mediator.Send(command, cancellationToken);

        return result.Match(
            _ => Ok(result.Value.MapToListGenresResponse()),
            errors => Problem(errors));
    }
}