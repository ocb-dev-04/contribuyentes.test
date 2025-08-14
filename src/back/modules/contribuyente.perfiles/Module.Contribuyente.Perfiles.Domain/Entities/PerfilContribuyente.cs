using Global.Sources.ValueObjects.Values;
using Module.Contribuyente.Perfiles.Domain.Enums;

namespace Module.Contribuyente.Perfiles.Domain.Entities;

public sealed partial class PerfilContribuyente
{
    public UlidObject Id { get; init; }
    public IdentityType Tipo { get; init; }
    public DateTimeOffset CreadoEnUtc { get; init; } = DateTimeOffset.UtcNow;

    public StringObject Nombre { get; private set; }
    public StringObject RncCedula { get; private set; }
    public BooleanObject EstaActivo { get; private set; } = BooleanObject.CreateAsTrue();

    internal PerfilContribuyente()
    {
        
    }

    private PerfilContribuyente(
        UlidObject id,
        IdentityType tipo,
        StringObject nombre,
        StringObject rncCedula)
    {
        this.Id = id;
        this.Tipo = tipo;
        this.Nombre = nombre;
        this.RncCedula = rncCedula;
    }
}
