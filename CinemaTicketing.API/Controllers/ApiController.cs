using CinemaTicketing.API.Common.Http;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CinemaTicketing.API.Controllers;

[ApiController]
public class ApiController : ControllerBase
{
    protected IActionResult Problem(List<Error> errors)
    {
        if (errors.Count is 0) return Problem();
        if (errors.All(error => error.Type == ErrorType.Validation)) return ValidationProblem(errors);

        HttpContext.Items[HttpContextItemKey.Errors] = errors;

        var fistError = errors.First();

        return Problem(fistError);
    }

    private IActionResult Problem(Error fistError)
    {
        var statusCode = fistError.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };
        return Problem(statusCode: statusCode, detail: fistError.Description);
    }

    private IActionResult ValidationProblem(List<Error> errors)
    {
        var modelStateDictionary = new ModelStateDictionary();

        foreach (var error in errors)
            modelStateDictionary.AddModelError(
                error.Code,
                error.Description);

        return ValidationProblem(modelStateDictionary);
    }
}