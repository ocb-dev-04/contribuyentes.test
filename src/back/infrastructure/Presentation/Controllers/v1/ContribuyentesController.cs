using MediatR;
using Global.Sources.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Global.Sources.Extensions;
using Global.Sources.ResultPattern;
using Module.Contribuyente.Perfiles.Features.UseCases.Collection;

namespace Presentation.Controllers.v1;

[ApiController]
[Route("api/v1/contribuyentes")]
[Produces("application/json")]
public sealed class ContribuyentesController : BaseController
{
    public ContribuyentesController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    [ProducesResponseType(typeof(PaginatedCollection<GetPerfilesCollectionQueryResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCollection(
        [FromQuery] GetPerfilesCollectionQuery query,
        CancellationToken cancellationToken)
    {
        Result<PaginatedCollection<GetPerfilesCollectionQueryResponse>> response 
            = await _sender.Send(query, cancellationToken);

        return response.Match(Ok, HandleErrorResults);
    }
}
