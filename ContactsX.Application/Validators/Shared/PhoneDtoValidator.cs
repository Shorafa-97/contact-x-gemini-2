using FastEndpoints;
using FluentValidation;
using ContactsX.Application.DTOs.Shared;

namespace ContactsX.Application.Validators.Shared;

public class PhoneDtoValidator : Validator<PhoneDto>
{
    public PhoneDtoValidator()
    {
        RuleFor(x => x.Value)
            .NotEmpty().WithMessage("Phone number is required.")
            .MaximumLength(20).WithMessage("Phone number cannot exceed 20 characters.")
            .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("A valid international phone number format is required.");
    }
}
