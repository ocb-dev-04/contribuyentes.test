using FluentValidation;
using Global.Sources.Constants;

namespace Modulo.Comprobante.Registros.Features.UseCases.CollectionByContribuyente;

internal sealed class GetComprobanteCollectionByContribuyenteQueryValidator
    : AbstractValidator<GetComprobanteCollectionByContribuyenteQuery>
{
    public GetComprobanteCollectionByContribuyenteQueryValidator()
    {
        RuleFor(x => x.ContribuyenteId)
            .Cascade(CascadeMode.Continue)
            .NotEmpty()
                .WithMessage(ValidationsConstants.FieldCantBeEmpty)
            .NotNull()
                .WithMessage(ValidationsConstants.RequiredField)
            .Length(26)
                .WithMessage(ValidationsConstants.InvalidId);

        RuleFor(x => x.PageNumber)
            .Cascade(CascadeMode.Continue)
            .NotEmpty()
                .WithMessage(ValidationsConstants.FieldCantBeEmpty)
            .NotNull()
                .WithMessage(ValidationsConstants.RequiredField)
            .GreaterThan(0)
                .WithMessage(ValidationsConstants.PageNumberCantBeZero);
    }
}