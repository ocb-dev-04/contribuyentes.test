using Global.Sources.Extensions;
using Module.Contribuyente.Perfiles.Domain.Entities;

namespace Module.Contribuyente.Perfiles.Features.UseCases.Collection;

public sealed record GetPerfilesCollectionQueryResponse(
    string Id,
    string Tipo,
    string Nombre,
    string RncCedula,
    DateTimeOffset CreadoEnUtc)
{
    public static GetPerfilesCollectionQueryResponse MapFromEntity(PerfilContribuyente entity)
        => new(
            entity.Id.Value,
            entity.Tipo.GetDescription(),
            entity.Nombre.Value,
            entity.RncCedula.Value,
            entity.CreadoEnUtc);
}