using ContactsX.Application.DTOs.Entity;
using ContactsX.Application.Features.Entities.Commands;
using ContactsX.Application.DTOs.Shared;

using ContactsX.Application.Features.Entities.Handlers;
using ContactsX.Application.Features.Entities.Queries;
using ContactsX.Domain.Entities;
using ContactsX.Domain.ValueOpjects;
using ContactsX.Infrastructure.Repositories;
using ContactsX.Persistence.DatabBaseContext;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ContactsX.Tests;

public class EntityHandlerTests
{
    private AppDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var context = new AppDbContext(options);
        context.Database.EnsureCreated();
        return context;
    }

    [Fact]
    public async Task CreateEntityHandler_ShouldAddEntityToDatabase()
    {
        // Arrange
        var context = GetDbContext();
        var repository = new Repository<Entity>(context);
        var handler = new CreateEntityHandler(repository);

        var dto = new CreateEntityDto(
            Guid.Empty,
            "Test Entity",
            "كيان تجريبي",
            "Public",
            "Egypt",
            "Tech",
            "REG123",
            null,
            new List<AddressDto> { new AddressDto("Office", "Cairo", true) },
            new List<ContactPointDto> { new ContactPointDto("Support", "support@test.com", "123", "Egypt") },
            10,
            true,
            DateTime.UtcNow,
            DateTime.UtcNow
        );
        var command = new CreateEntityCommand(dto);

        // Act
        var resultId = await handler.Handle(command, CancellationToken.None);

        // Assert
        resultId.Should().NotBeEmpty();
        var entity = await context.Entities.FindAsync(resultId);
        entity.Should().NotBeNull();
        entity!.NameEn.Should().Be("Test Entity");
        entity.Type.Should().Be(EntityType.Public);
    }

    [Fact]
    public async Task UpdateEntityHandler_ShouldUpdateExistingEntity()
    {
        // Arrange
        var context = GetDbContext();
        var repository = new Repository<Entity>(context);
        var id = Guid.NewGuid();
        var entity = new Entity { Id = id, NameEn = "Old Name", Type = EntityType.Private };
        context.Entities.Add(entity);
        await context.SaveChangesAsync();

        var handler = new UpdateEntityHandler(repository);
        var dto = new UpdateEntityDto(id, "New Name", null, "Public", null, null, null, null, null, null, 20, true, DateTime.UtcNow, DateTime.UtcNow);
        var command = new UpdateEntityCommand(id, dto);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeTrue();
        var updated = await context.Entities.FindAsync(id);
        updated!.NameEn.Should().Be("New Name");
        updated.Type.Should().Be(EntityType.Public);
    }

    [Fact]
    public async Task UpdateEntityHandler_ShouldReturnFalse_WhenNotFound()
    {
        // Arrange
        var context = GetDbContext();
        var repository = new Repository<Entity>(context);
        var handler = new UpdateEntityHandler(repository);
        var dto = new UpdateEntityDto(Guid.NewGuid(), "Name", null, "Public", null, null, null, null, null, null, 0, true, DateTime.UtcNow, DateTime.UtcNow);
        var command = new UpdateEntityCommand(Guid.NewGuid(), dto);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task DeleteEntityHandler_ShouldRemoveEntity()
    {
        // Arrange
        var context = GetDbContext();
        var repository = new Repository<Entity>(context);
        var id = Guid.NewGuid();
        context.Entities.Add(new Entity { Id = id, NameEn = "To Delete" });
        await context.SaveChangesAsync();

        var handler = new DeleteEntityHandler(repository);
        var command = new DeleteEntityCommand(id);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeTrue();
        var deleted = await context.Entities.FindAsync(id);
        deleted.Should().BeNull();
    }

    [Fact]
    public async Task GetEntityByIdHandler_ShouldReturnDto()
    {
        // Arrange
        var context = GetDbContext();
        var repository = new Repository<Entity>(context);
        var id = Guid.NewGuid();
        context.Entities.Add(new Entity { Id = id, NameEn = "Entity 1", Type = EntityType.NGO });
        await context.SaveChangesAsync();

        var handler = new GetEntityByIdHandler(repository);
        var query = new GetEntityByIdQuery(id);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result!.NameEn.Should().Be("Entity 1");
        result.EntityType.Should().Be("NGO");
    }
}
