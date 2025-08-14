using MediatR;
using Global.Sources.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Global.Sources.Extensions;
using Global.Sources.ResultPattern;
using Modulo.Comprobante.Registros.Features.UseCases.TotalItbsSum;
using Modulo.Comprobante.Registros.Features.UseCases.CollectionByContribuyente;

namespace Presentation.Controllers.v1;

[ApiController]
[Route("api/v1/registros")]
[Produces("application/json")]
public sealed class RegistrosController : BaseController
{
    public RegistrosController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    [ProducesResponseType(typeof(PaginatedCollection<ComprobanteRegistroResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCollection(
        [FromQuery] GetComprobanteCollectionByContribuyenteQuery query,
        CancellationToken cancellationToken)
    {
        Result<PaginatedCollection<ComprobanteRegistroResponse>> response
            = await _sender.Send(query, cancellationToken);

        return response.Match(Ok, HandleErrorResults);
    }

    [HttpGet("total-itbs-sum")]
    [ProducesResponseType(typeof(PaginatedCollection<ComprobanteRegistroResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetTotalItbsSum(
        [FromQuery] GetRegistrosTotalItbsSumQuery query,
        CancellationToken cancellationToken)
    {
        Result<GetRegistrosTotalItbsSumQueryResponse> response
            = await _sender.Send(query, cancellationToken);

        return response.Match(Ok, HandleErrorResults);
    }
}
