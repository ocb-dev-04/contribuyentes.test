using Global.Sources.ValueObjects.Values;
using Module.Contribuyente.Perfiles.Domain.Enums;

namespace Module.Contribuyente.Perfiles.Domain.Entities;

public sealed partial class PerfilContribuyente
{
    public static PerfilContribuyente Create(
        IdentityType tipo,
        StringObject nombre,
        StringObject rncCedula)
        => new(
            UlidObject.New(),
            tipo,
            nombre,
            rncCedula);

    public void UpdateNombre(StringObject nombre)
    {
        if (this.Nombre.Equals(nombre))
            return;

        this.Nombre = nombre;
    }

    public void SetAsActice()
    {
        if (this.EstaActivo.Value)
            return;

        this.EstaActivo = BooleanObject.CreateAsTrue();
    }

    public void SetAsInactive()
    {
        if (!this.EstaActivo.Value)
            return;

        this.EstaActivo = BooleanObject.CreateAsFalse();
    }
}