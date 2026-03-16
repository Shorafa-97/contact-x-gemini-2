using ContactsX.Application.DTOs.Duplicate;
using ContactsX.Application.Features.Duplicates.Commands;
using ContactsX.Application.Features.Duplicates.Handlers;
using ContactsX.Application.Features.Duplicates.Queries;
using ContactsX.Application.Validators.Duplicate;
using ContactsX.Application.Interfaces.Repositories;

using ContactsX.Domain.Entities;
using FluentAssertions;
using FluentValidation.TestHelper;
using Moq;

using System.Linq.Expressions;
using Xunit;

namespace ContactsX.Tests;

public class DuplicateHandlerTests
{
    private readonly Mock<IRepository<DuplicateCandidate>> _repositoryMock;

    public DuplicateHandlerTests()
    {
        _repositoryMock = new Mock<IRepository<DuplicateCandidate>>();
    }

    [Fact]
    public async Task GetDuplicatesHandler_ShouldReturnDtos()
    {
        // Arrange
        var candidates = new List<DuplicateCandidate>
        {
            new() { Id = Guid.NewGuid(), EntityType = "contact", MatchScore = 90, CreatedAt = DateTime.UtcNow, Status = "pending" }
        };
        _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(candidates);
        var handler = new GetDuplicatesHandler(_repositoryMock.Object);

        // Act
        var result = await handler.Handle(new GetDuplicatesQuery(), CancellationToken.None);

        // Assert
        result.Should().HaveCount(1);
        result.First().EntityType.Should().Be("contact");
    }

    [Fact]
    public async Task GetDuplicateMetricsHandler_ShouldCalculateCorrectly()
    {
        // Arrange
        var candidates = new List<DuplicateCandidate>
        {
            new() { MatchScore = 90, Status = "pending" }, // High
            new() { MatchScore = 60, Status = "merged" },  // Medium, Resolved
            new() { MatchScore = 30, Status = "pending" }  // Low
        };
        _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(candidates);
        var handler = new GetDuplicateMetricsHandler(_repositoryMock.Object);

        // Act
        var result = await handler.Handle(new GetDuplicateMetricsQuery(), CancellationToken.None);

        // Assert
        result.Total.Should().Be(3);
        result.HighConfidence.Should().Be(1);
        result.MediumConfidence.Should().Be(1);
        result.LowConfidence.Should().Be(1);
        result.Pending.Should().Be(2);
        result.Resolved.Should().Be(1);
        result.ResolutionRate.Should().Be(33); // (1 * 100) / 3
    }

    [Fact]
    public async Task MergeDuplicateHandler_ShouldReturnTrue_WhenExists()
    {
        // Arrange
        var id = Guid.NewGuid();
        var candidate = new DuplicateCandidate { Id = id, Status = "pending" };
        _repositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(candidate);
        var handler = new MergeDuplicateHandler(_repositoryMock.Object);

        // Act
        var result = await handler.Handle(new MergeDuplicateCommand(id, new MergeRequest(Guid.NewGuid())), CancellationToken.None);

        // Assert
        result.Should().BeTrue();
        candidate.Status.Should().Be("merged");
        candidate.ResolvedAt.Should().NotBeNull();
    }

    [Fact]
    public async Task MergeDuplicateHandler_ShouldReturnFalse_WhenNotExists()
    {
        // Arrange
        var id = Guid.NewGuid();
        _repositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((DuplicateCandidate)null);
        var handler = new MergeDuplicateHandler(_repositoryMock.Object);

        // Act
        var result = await handler.Handle(new MergeDuplicateCommand(id, new MergeRequest(Guid.NewGuid())), CancellationToken.None);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task DismissDuplicateHandler_ShouldReturnTrue_WhenExists()
    {
        // Arrange
        var id = Guid.NewGuid();
        var candidate = new DuplicateCandidate { Id = id, Status = "pending" };
        _repositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(candidate);
        var handler = new DismissDuplicateHandler(_repositoryMock.Object);

        // Act
        var result = await handler.Handle(new DismissDuplicateCommand(id), CancellationToken.None);

        // Assert
        result.Should().BeTrue();
        candidate.Status.Should().Be("dismissed");
        candidate.ResolvedAt.Should().NotBeNull();
    }

    [Fact]
    public async Task DismissDuplicateHandler_ShouldReturnFalse_WhenNotExists()
    {
        // Arrange
        var id = Guid.NewGuid();
        _repositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((DuplicateCandidate)null);
        var handler = new DismissDuplicateHandler(_repositoryMock.Object);

        // Act
        var result = await handler.Handle(new DismissDuplicateCommand(id), CancellationToken.None);

        // Assert
        result.Should().BeFalse();
    }
}

public class DuplicateValidatorTests
{
    private readonly MergeRequestValidator _validator = new();

    [Fact]
    public void MergeRequestValidator_ShouldHaveError_WhenMasterIdIsEmpty()
    {
        var model = new MergeRequest(Guid.Empty);
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.MasterId);
    }

    [Fact]
    public void MergeRequestValidator_ShouldHaveError_WhenMasterIdIsNull()
    {
        var model = new MergeRequest(null);
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.MasterId);
    }

    [Fact]
    public void MergeRequestValidator_ShouldNotHaveError_WhenMasterIdIsValid()
    {
        var model = new MergeRequest(Guid.NewGuid());
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(x => x.MasterId);
    }
}

