using Global.Sources.ValueObjects.Values;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Modulo.Comprobante.Registros.Domain.Entities;

public sealed partial class ComprobanteRegistro
{
    public UlidObject Id { get; init; }
    public UlidObject PerfilContribuyenteId { get; init; }

    public StringObject NCF { get; init; }
    public DecimalObject Monto { get; init; }
    public DecimalObject Itbis18 { get; init; }
    public DateTimeOffset CreadoEnUtc { get; init; } = DateTimeOffset.UtcNow;

    [NotMapped]
    public decimal Itbis18Value => EF.Property<decimal>(this, "Itbis18");

    internal ComprobanteRegistro()
    {
        
    }

    private ComprobanteRegistro(
        UlidObject id,
        UlidObject perfilContribuyenteId,
        StringObject ncf,
        DecimalObject monto)
    {
        this.Id = id;
        this.PerfilContribuyenteId = perfilContribuyenteId;
        this.NCF = ncf;
        this.Monto = monto;
        this.Itbis18 = DecimalObject.Create(this.Monto.Value * 0.18m);
    }
}
