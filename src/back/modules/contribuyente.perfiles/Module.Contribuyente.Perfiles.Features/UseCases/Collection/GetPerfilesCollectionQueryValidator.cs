using FluentValidation;
using Global.Sources.Constants;

namespace Module.Contribuyente.Perfiles.Features.UseCases.Collection;

internal sealed class GetPerfilesCollectionQueryValidator
    : AbstractValidator<GetPerfilesCollectionQuery>
{
    public GetPerfilesCollectionQueryValidator()
    {
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
