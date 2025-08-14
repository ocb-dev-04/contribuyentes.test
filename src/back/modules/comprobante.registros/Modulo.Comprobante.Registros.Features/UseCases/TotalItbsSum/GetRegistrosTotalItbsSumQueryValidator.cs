using FluentValidation;
using Global.Sources.Constants;

namespace Modulo.Comprobante.Registros.Features.UseCases.TotalItbsSum;

internal sealed class GetRegistrosTotalItbsSumQueryValidator
    : AbstractValidator<GetRegistrosTotalItbsSumQuery>
{
    public GetRegistrosTotalItbsSumQueryValidator()
    {
        RuleFor(x => x.ContribuyenteId)
            .Cascade(CascadeMode.Continue)
            .NotEmpty()
                .WithMessage(ValidationsConstants.FieldCantBeEmpty)
            .NotNull()
                .WithMessage(ValidationsConstants.RequiredField)
            .Length(26)
                .WithMessage(ValidationsConstants.InvalidId);
    }
}