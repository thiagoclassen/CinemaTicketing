using Microsoft.AspNetCore.Mvc;

namespace CinemaTicketing.API.Controllers;

[ApiController]
public class ErrorsController : ControllerBase
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route(ApiEndpoints.Errors.Base)]
    public IActionResult Error()
    {
        return Problem();
    }
}