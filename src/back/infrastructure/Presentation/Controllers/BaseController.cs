using MediatR;
using Microsoft.AspNetCore.Mvc;
using Global.Sources.ResultPattern;

namespace Presentation.Controllers;

public class BaseController : ControllerBase
{
    protected readonly ISender _sender;

    protected BaseController(ISender sender)
        => _sender = sender ?? throw new ArgumentNullException(nameof(sender));

    /// <summary>
    /// Handles error results based on the provided <see cref="Error"/> object.
    /// </summary>
    /// <param name="error"></param>
    /// <returns></returns>
    protected IActionResult HandleErrorResults(Error error)
        => error.StatusCode switch
        {
            304 => StatusCode(error.StatusCode, new { error.Translation, error.Description }),
            400 => BadRequest(new { error.Translation, error.Description }),
            401 => Unauthorized(),
            404 => NotFound(new { error.Translation, error.Description }),
            _ => StatusCode(error.StatusCode)
        };
}