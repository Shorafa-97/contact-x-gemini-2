using ContactsX.Application.DTOs.Entity;
using FluentValidation;

namespace ContactsX.Application.Validators;

public class UpdateEntityDtoValidator : AbstractValidator<UpdateEntityDto>
{
    public UpdateEntityDtoValidator()
    {
        RuleFor(x => x.NameEn).NotEmpty().WithMessage("nameEn cannot be null or empty.");
        RuleFor(x => x.EntityType).NotEmpty().WithMessage("entityType must be a valid string.");
    }
}
