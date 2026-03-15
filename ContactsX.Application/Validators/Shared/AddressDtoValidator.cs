using FastEndpoints;
using FluentValidation;
using ContactsX.Application.DTOs.Shared;

namespace ContactsX.Application.Validators.Shared;

public class AddressDtoValidator : Validator<AddressDto>
{
    public AddressDtoValidator()
    {
        RuleFor(x => x.Type)
            .NotEmpty().WithMessage("Address type is required.");

        RuleFor(x => x.Value)
            .NotEmpty().WithMessage("Address value is required.");
    }
}
