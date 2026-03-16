using ContactsX.Application.DTOs.Dashboard;
using ContactsX.Application.Features.Dashboards.Handlers;
using ContactsX.Application.Features.Dashboards.Queries;
using ContactsX.Application.Interfaces.Repositories;
using ContactsX.Domain.Entities;
using FluentAssertions;
using Moq;
using Xunit;

namespace ContactsX.Tests;

public class DashboardHandlerTests
{
    private readonly Mock<IRepository<Contact>> _contactRepoMock = new();
    private readonly Mock<IRepository<Entity>> _entityRepoMock = new();
    private readonly Mock<IRepository<DuplicateCandidate>> _duplicateRepoMock = new();

    [Fact]
    public async Task GetDashboardStatsHandler_ShouldReturnCorrectStats()
    {
        // Arrange
        var contacts = new List<Contact> { new() { IsActive = true }, new() { IsActive = false } };
        var entities = new List<Entity> { new() { IsActive = true, ProfileCompleteness = 80 } };
        var duplicates = new List<DuplicateCandidate> { new() { Status = "pending" } };

        _contactRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(contacts);
        _entityRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(entities);
        _duplicateRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(duplicates);

        var handler = new GetDashboardStatsHandler(_contactRepoMock.Object, _entityRepoMock.Object, _duplicateRepoMock.Object);

        // Act
        var result = await handler.Handle(new GetDashboardStatsQuery(), CancellationToken.None);

        // Assert
        result.TotalContacts.Should().Be(2);
        result.ActiveContacts.Should().Be(1);
        result.TotalEntities.Should().Be(1);
        result.ActiveEntities.Should().Be(1);
        result.AverageCompleteness.Should().Be(80);
        result.DuplicateCandidates.Should().Be(1);
    }

    [Fact]
    public async Task OtherDashboardHandlers_ShouldReturnEmptyData()
    {
        var handler = new GetDashboardDataHandlers();

        (await handler.Handle(new GetExecutiveDashboardQuery(), default)).Should().NotBeNull();
        (await handler.Handle(new GetGovernanceDashboardQuery(), default)).Should().NotBeNull();
        (await handler.Handle(new GetOperationalDashboardQuery(), default)).Should().NotBeNull();
        (await handler.Handle(new GetDuplicateMetricsQuery(), default)).Should().NotBeNull();
    }
}
