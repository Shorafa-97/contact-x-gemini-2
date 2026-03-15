using ContactsX.Application.Features.Entities.Commands;
using FluentValidation;

namespace ContactsX.Application.Validators;

public class UpdateEntityCommandValidator : AbstractValidator<UpdateEntityCommand>
{
    public UpdateEntityCommandValidator()
    {
        RuleFor(x => x.EntityDto.NameEn).NotEmpty().WithMessage("nameEn cannot be null or empty.");
        RuleFor(x => x.EntityDto.EntityType).NotEmpty().WithMessage("entityType must be a valid string.");
    }
}
