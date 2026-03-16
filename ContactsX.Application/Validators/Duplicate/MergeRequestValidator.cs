using FluentValidation;
using ContactsX.Application.DTOs.Duplicate;

namespace ContactsX.Application.Validators.Duplicate;

public class MergeRequestValidator : AbstractValidator<MergeRequest>
{
    public MergeRequestValidator()
    {
        RuleFor(x => x.MasterId)
            .NotEmpty().WithMessage("MasterId is required.")
            .NotEqual(Guid.Empty).WithMessage("MasterId cannot be an empty Guid.");
    }
}
