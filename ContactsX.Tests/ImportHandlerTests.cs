using ContactsX.Application.DTOs.Import;
using ContactsX.Application.Features.Import.Commands;
using ContactsX.Application.Features.Import.Handlers;
using ContactsX.Application.Interfaces.Repositories;
using ContactsX.Domain.Entities;
using FluentAssertions;
using Moq;
using System.Text.Json;
using Xunit;

namespace ContactsX.Tests;

public class ImportHandlerTests
{
    private readonly Mock<IRepository<Contact>> _contactRepoMock = new();
    private readonly Mock<IRepository<Entity>> _entityRepoMock = new();

    [Fact]
    public async Task ImportContactsHandler_ShouldHandlePartialSuccess()
    {
        // Arrange
        var records = new[]
        {
            JsonSerializer.Deserialize<JsonElement>("{\"firstName\": \"John\", \"lastName\": \"Doe\", \"email\": \"john@example.com\"}"),
            JsonSerializer.Deserialize<JsonElement>("{\"lastName\": \"Doe\"}") // Missing firstName
        };

        var handler = new ImportContactsHandler(_contactRepoMock.Object);
        var command = new ImportContactsCommand(records);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Imported.Should().Be(1);
        result.Errors.Should().HaveCount(1);
        result.Errors[0].Should().Contain("FirstName is required");
        _contactRepoMock.Verify(r => r.AddAsync(It.IsAny<Contact>()), Times.Once);
        _contactRepoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task ImportEntitiesHandler_ShouldHandlePartialSuccess()
    {
        // Arrange
        var records = new[]
        {
            JsonSerializer.Deserialize<JsonElement>("{\"nameEn\": \"Valid Entity\", \"country\": \"Egypt\"}"),
            JsonSerializer.Deserialize<JsonElement>("{\"country\": \"Sudan\"}") // Missing nameEn
        };

        var handler = new ImportEntitiesHandler(_entityRepoMock.Object);
        var command = new ImportEntitiesCommand(records);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Imported.Should().Be(1);
        result.Errors.Should().HaveCount(1);
        result.Errors[0].Should().Contain("NameEn is required");
        _entityRepoMock.Verify(r => r.AddAsync(It.IsAny<Entity>()), Times.Once);
        _entityRepoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }
}
