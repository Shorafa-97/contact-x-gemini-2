using ContactsX.Application.DTOs.Entity;
using FluentValidation;

namespace ContactsX.Application.Validators;

public class CreateEntityDtoValidator : AbstractValidator<CreateEntityDto>
{
    public CreateEntityDtoValidator()
    {
        RuleFor(x => x.NameEn).NotEmpty().WithMessage("nameEn cannot be null or empty.");
        RuleFor(x => x.EntityType).NotEmpty().WithMessage("entityType must be a valid string.");
    }
}
