using FluentValidation;
using ContactsX.Application.Features.Import.Commands;

namespace ContactsX.Application.Validators.Import;

public class ImportContactsValidator : AbstractValidator<ImportContactsCommand>
{
    public ImportContactsValidator()
    {
        RuleFor(x => x.Records)
            .NotNull()
            .NotEmpty().WithMessage("The import list must contain at least one item.");
    }
}

public class ImportEntitiesValidator : AbstractValidator<ImportEntitiesCommand>
{
    public ImportEntitiesValidator()
    {
        RuleFor(x => x.Records)
            .NotNull()
            .NotEmpty().WithMessage("The import list must contain at least one item.");
    }
}
