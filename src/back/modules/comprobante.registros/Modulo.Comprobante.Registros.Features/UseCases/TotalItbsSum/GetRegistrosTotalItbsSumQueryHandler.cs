using MediatR;
using Common.Sources.Abstractions;
using Global.Sources.ResultPattern;
using Global.Sources.ValueObjects.Values;

namespace Modulo.Comprobante.Registros.Features.UseCases.TotalItbsSum;

internal sealed class GetRegistrosTotalItbsSumQueryHandler
    : IRequestHandler<GetRegistrosTotalItbsSumQuery, Result<GetRegistrosTotalItbsSumQueryResponse>>
{
    private readonly IUnitOfwork _unitOfwork;

    public GetRegistrosTotalItbsSumQueryHandler(IUnitOfwork unitOfwork)
        => _unitOfwork = unitOfwork ?? throw new ArgumentNullException(nameof(unitOfwork));

    public async Task<Result<GetRegistrosTotalItbsSumQueryResponse>> Handle(GetRegistrosTotalItbsSumQuery request, CancellationToken cancellationToken)
    {
        UlidObject contribuyenteIdAsObjet = UlidObject.Create(request.ContribuyenteId);
        var totalSum = await _unitOfwork.ComprobanteRegistro.TotalItebisSumAsync(contribuyenteIdAsObjet, cancellationToken);
        
        return new GetRegistrosTotalItbsSumQueryResponse(totalSum);
    }
}