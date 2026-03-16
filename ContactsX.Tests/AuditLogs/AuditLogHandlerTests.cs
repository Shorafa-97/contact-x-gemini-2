using ContactsX.Application.DTOs.AuditLog;
using ContactsX.Application.Features.AuditLogs.Handlers;
using ContactsX.Application.Features.AuditLogs.Queries;
using ContactsX.Application.Interfaces.Repositories;
using ContactsX.Domain.Entities;
using FluentAssertions;
using Moq;
using Xunit;

namespace ContactsX.Tests.AuditLogs;

public class AuditLogHandlerTests
{
    private readonly Mock<IRepository<AuditLog>> _auditLogRepoMock = new();

    [Fact]
    public async Task GetAuditLogsHandler_ShouldApplyFiltersAndLimit()
    {
        // Arrange
        var logs = new List<AuditLog>
        {
            new AuditLog { Id = Guid.NewGuid(), EntityType = "contact", Action = "create", CreatedAt = DateTime.UtcNow.AddMinutes(-10) },
            new AuditLog { Id = Guid.NewGuid(), EntityType = "contact", Action = "update", CreatedAt = DateTime.UtcNow.AddMinutes(-5) },
            new AuditLog { Id = Guid.NewGuid(), EntityType = "entity", Action = "create", CreatedAt = DateTime.UtcNow.AddMinutes(-1) }
        };

        _auditLogRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(logs);

        var handler = new GetAuditLogsHandler(_auditLogRepoMock.Object);
        var query = new GetAuditLogsQuery(EntityType: "contact", Action: "update", Limit: 1);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().HaveCount(1);
        result.First().Action.Should().Be("update");
        result.First().EntityType.Should().Be("contact");
    }

    [Fact]
    public async Task GetAuditLogsHandler_ShouldOrderByDateDescending()
    {
        // Arrange
        var logs = new List<AuditLog>
        {
            new AuditLog { Id = Guid.NewGuid(), CreatedAt = DateTime.UtcNow.AddMinutes(-10) },
            new AuditLog { Id = Guid.NewGuid(), CreatedAt = DateTime.UtcNow }
        };

        _auditLogRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(logs);

        var handler = new GetAuditLogsHandler(_auditLogRepoMock.Object);
        var query = new GetAuditLogsQuery();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.First().CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
    }
}
