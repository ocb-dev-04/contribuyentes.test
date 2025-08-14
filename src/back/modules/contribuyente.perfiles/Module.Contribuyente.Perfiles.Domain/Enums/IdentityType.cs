using System.ComponentModel;

namespace Module.Contribuyente.Perfiles.Domain.Enums;

public enum IdentityType
{
    [Description("Persona Física")]
    PersonaFisica = 1,

    [Description("Persona Jurídica")]
    PersonaJuridica = 2
}
