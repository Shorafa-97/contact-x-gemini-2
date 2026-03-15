using ContactsX.Application.DTOs.Entity;
using ContactsX.Application.Validators;
using ContactsX.Application.DTOs.Shared;

using FluentValidation.TestHelper;
using Xunit;

namespace ContactsX.Tests;

public class EntityValidatorTests
{
    private readonly CreateEntityDtoValidator _createValidator = new();
    private readonly UpdateEntityDtoValidator _updateValidator = new();

    [Fact]
    public void CreateEntityDtoValidator_ShouldHaveError_WhenNameEnIsEmpty()
    {
        var dto = new CreateEntityDto(Guid.Empty, "", null, "Public", null, null, null, null, null, null, 0, true, DateTime.UtcNow, DateTime.UtcNow);
        var result = _createValidator.TestValidate(dto);
        result.ShouldHaveValidationErrorFor(x => x.NameEn);
    }

    [Fact]
    public void CreateEntityDtoValidator_ShouldHaveError_WhenEntityTypeIsEmpty()
    {
        var dto = new CreateEntityDto(Guid.Empty, "Name", null, "", null, null, null, null, null, null, 0, true, DateTime.UtcNow, DateTime.UtcNow);
        var result = _createValidator.TestValidate(dto);
        result.ShouldHaveValidationErrorFor(x => x.EntityType);
    }

    [Fact]
    public void CreateEntityDtoValidator_ShouldNotHaveError_WhenValid()
    {
        var dto = new CreateEntityDto(Guid.Empty, "Name", null, "Public", null, null, null, null, null, null, 0, true, DateTime.UtcNow, DateTime.UtcNow);
        var result = _createValidator.TestValidate(dto);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
