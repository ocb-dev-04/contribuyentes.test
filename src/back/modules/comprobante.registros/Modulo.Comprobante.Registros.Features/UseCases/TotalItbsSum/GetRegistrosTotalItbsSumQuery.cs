using Global.Sources.Abstractions.Messaging;

namespace Modulo.Comprobante.Registros.Features.UseCases.TotalItbsSum;

public sealed record GetRegistrosTotalItbsSumQuery(string ContribuyenteId)
    : IQuery<GetRegistrosTotalItbsSumQueryResponse>;
