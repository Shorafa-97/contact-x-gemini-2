using FastEndpoints;
using FluentValidation;
using ContactsX.Application.DTOs.Relation;

namespace ContactsX.Application.Validators.Relation;

public class CreateRelationValidator : Validator<CreateRelationDto>
{
    public CreateRelationValidator()
    {
        RuleFor(x => x.TargetEntityId)
            .NotEmpty().WithMessage("Target entity ID is required and cannot be an empty GUID.");

        RuleFor(x => x.Role)
            .NotEmpty().WithMessage("Role is required.");
    }
}
