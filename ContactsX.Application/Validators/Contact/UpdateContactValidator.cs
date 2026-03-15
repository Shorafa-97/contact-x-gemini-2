using FastEndpoints;
using FluentValidation;
using ContactsX.Application.DTOs.Contact;
using ContactsX.Application.Validators.Shared;

namespace ContactsX.Application.Validators.Contact;

public class UpdateContactValidator : Validator<UpdateContactDto>
{
    public UpdateContactValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name cannot be empty if provided.")
            .MaximumLength(100).WithMessage("First name cannot exceed 100 characters.")
            .When(x => x.FirstName != null);

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name cannot be empty if provided.")
            .MaximumLength(100).WithMessage("Last name cannot exceed 100 characters.")
            .When(x => x.LastName != null);

        RuleFor(x => x.ContactType)
            .IsInEnum().WithMessage("Valid contact type is required.")
            .When(x => x.ContactType.HasValue);

        RuleFor(x => x.Gender)
            .IsInEnum().WithMessage("Valid gender is required.")
            .When(x => x.Gender.HasValue);

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
