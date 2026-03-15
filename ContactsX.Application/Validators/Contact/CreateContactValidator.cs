using FastEndpoints;
using FluentValidation;
using ContactsX.Application.DTOs.Contact;
using ContactsX.Application.Validators.Shared;

namespace ContactsX.Application.Validators.Contact;

public class CreateContactValidator : Validator<CreateContactDto>
{
    public CreateContactValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(100).WithMessage("First name cannot exceed 100 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(100).WithMessage("Last name cannot exceed 100 characters.");

        RuleFor(x => x.ContactType)
            .IsInEnum().WithMessage("Valid contact type is required.");

        RuleFor(x => x.Gender)
            .IsInEnum().When(x => x.Gender.HasValue).WithMessage("Valid gender is required.");

        RuleFor(x => x.DateOfBirth)
            .Must(BeAValidPastDate).When(x => !string.IsNullOrEmpty(x.DateOfBirth))
            .WithMessage("Date of birth must be a valid date in the past.");

        RuleForEach(x => x.Emails)
            .SetValidator(new EmailDtoValidator()!)
            .When(x => x.Emails != null);

        RuleForEach(x => x.Phones)
            .SetValidator(new PhoneDtoValidator()!)
            .When(x => x.Phones != null);

        RuleForEach(x => x.Addresses)
            .SetValidator(new AddressDtoValidator()!)
            .When(x => x.Addresses != null);
            
        RuleFor(x => x.Prefix).MaximumLength(20).When(x => x.Prefix != null);
        RuleFor(x => x.Suffix).MaximumLength(20).When(x => x.Suffix != null);
        RuleFor(x => x.FirstNameAr).MaximumLength(100).When(x => x.FirstNameAr != null);
        RuleFor(x => x.LastNameAr).MaximumLength(100).When(x => x.LastNameAr != null);
    }

    private bool BeAValidPastDate(string? date)
    {
        if (DateTime.TryParse(date, out var parsedDate))
        {
            return parsedDate < DateTime.UtcNow;
        }
        return false;
    }
}
