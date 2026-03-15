using FastEndpoints;
using FluentValidation;
using ContactsX.Application.DTOs.Shared;

namespace ContactsX.Application.Validators.Shared;

public class EmailDtoValidator : Validator<EmailDto>
{
    public EmailDtoValidator()
    {
        RuleFor(x => x.Value)
            .NotEmpty().WithMessage("Email value is required.")
            .EmailAddress().WithMessage("A valid email address format is required.");
    }
}
