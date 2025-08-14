using Global.Sources.ResultPattern;

namespace Module.Contribuyente.Perfiles.Domain.Errors;

public sealed class PerfilContribuyenteErrors
{
    public static Error NotFound
        = Error.NotFound("perfilNoEncontrado", "El perfil no pudo ser encontrado");

    public static Error AlreadyExist
        = Error.BadRequest("perfilYaExiste", "Este perfil ya existe");
}
