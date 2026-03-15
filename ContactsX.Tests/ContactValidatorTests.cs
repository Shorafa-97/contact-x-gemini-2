using ContactsX.Application.DTOs.Contact;
using ContactsX.Application.Validators.Contact;
using ContactsX.Domain.ValueOpjects;
using FluentAssertions;
using Xunit;

namespace ContactsX.Tests;

public class ContactValidatorTests
{
    private readonly CreateContactValidator _validator;

    public ContactValidatorTests()
    {
        _validator = new CreateContactValidator();
    }

    [Fact]
    public async Task CreateContactValidator_EmptyFirstName_ShouldHaveError()
    {
        // Arrange
        var dto = new CreateContactDto
        {
            FirstName = "",
            LastName = "Doe",
            ContactType = ContactType.Citizen
        };

        // Act
        var result = await _validator.ValidateAsync(dto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == nameof(dto.FirstName));
    }

    [Fact]
    public async Task CreateContactValidator_FutureDateOfBirth_ShouldHaveError()
    {
        // Arrange
        var dto = new CreateContactDto
        {
            FirstName = "John",
            LastName = "Doe",
            ContactType = ContactType.Citizen,
            DateOfBirth = DateTime.UtcNow.AddDays(1).ToString("yyyy-MM-dd")
        };

        // Act
        var result = await _validator.ValidateAsync(dto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == nameof(dto.DateOfBirth));
    }

    [Fact]
    public async Task CreateContactValidator_ValidDto_ShouldBeValid()
    {
        // Arrange
        var dto = new CreateContactDto
        {
            FirstName = "John",
            LastName = "Doe",
            ContactType = ContactType.Citizen,
            DateOfBirth = "1990-01-01"
        };

        // Act
        var result = await _validator.ValidateAsync(dto);

        // Assert
        result.IsValid.Should().BeTrue();
    }
}
