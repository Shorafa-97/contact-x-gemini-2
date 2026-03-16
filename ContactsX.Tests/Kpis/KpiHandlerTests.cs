using ContactsX.Application.DTOs.Contact;
using ContactsX.Application.Features.Kpis.Handlers;
using ContactsX.Application.Features.Kpis.Queries;
using ContactsX.Application.Interfaces.Repositories;
using ContactsX.Domain.Entities;
using FluentAssertions;
using Moq;
using System.Text.Json;
using Xunit;

namespace ContactsX.Tests.Kpis;

public class KpiHandlerTests
{
    private readonly Mock<IRepository<Contact>> _contactRepoMock = new();
    private readonly Mock<IRepository<Entity>> _entityRepoMock = new();
    private readonly Mock<IRepository<Relation>> _relationRepoMock = new();

    [Fact]
    public async Task GetWeakContactsHandler_ShouldFilterByCompletenessAndLimit()
    {
        // Arrange
        var contacts = new List<Contact>
        {
            new Contact { Id = Guid.NewGuid(), FirstName = "Weak1", ProfileCompleteness = 30, Classifications = "[]", Emails = "[]", Phones = "[]" },
            new Contact { Id = Guid.NewGuid(), FirstName = "Weak2", ProfileCompleteness = 40, Classifications = "[]", Emails = "[]", Phones = "[]" },
            new Contact { Id = Guid.NewGuid(), FirstName = "Strong", ProfileCompleteness = 80, Classifications = "[]", Emails = "[]", Phones = "[]" }
        };

        _contactRepoMock.Setup(r => r.FindAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Contact, bool>>>()))
            .ReturnsAsync(contacts.Where(c => c.ProfileCompleteness < 50));

        var handler = new GetWeakContactsHandler(_contactRepoMock.Object);
        var query = new GetWeakContactsQuery(1);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().HaveCount(1);
        result.First().FirstName.Should().Be("Weak1");
    }

    [Fact]
    public async Task GetOrphanContactsHandler_ShouldReturnContactsWithNoRelations()
    {
        // Arrange
        var contact1Id = Guid.NewGuid();
        var contact2Id = Guid.NewGuid();
        var contacts = new List<Contact>
        {
            new Contact { Id = contact1Id, FirstName = "Orphan", Classifications = "[]", Emails = "[]", Phones = "[]" },
            new Contact { Id = contact2Id, FirstName = "Related", Classifications = "[]", Emails = "[]", Phones = "[]" }
        };

        var relations = new List<Relation>
        {
            new Relation { ContactId = contact2Id }
        };

        _contactRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(contacts);
        _relationRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(relations);

        var handler = new GetOrphanContactsHandler(_contactRepoMock.Object, _relationRepoMock.Object);
        var query = new GetOrphanContactsQuery();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().HaveCount(1);
        result.First().FirstName.Should().Be("Orphan");
    }

    [Fact]
    public async Task GetVipIncompleteContactsHandler_ShouldFilterVipWithUnder100Completeness()
    {
        // Arrange
        var contacts = new List<Contact>
        {
            new Contact { Id = Guid.NewGuid(), FirstName = "VipIncomplete", ProfileCompleteness = 90, Classifications = "[\"VIP\"]", Emails = "[]", Phones = "[]" },
            new Contact { Id = Guid.NewGuid(), FirstName = "VipComplete", ProfileCompleteness = 100, Classifications = "[\"VIP\"]", Emails = "[]", Phones = "[]" },
            new Contact { Id = Guid.NewGuid(), FirstName = "StandardIncomplete", ProfileCompleteness = 90, Classifications = "[]", Emails = "[]", Phones = "[]" }
        };

        _contactRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(contacts);

        var handler = new GetVipIncompleteContactsHandler(_contactRepoMock.Object);
        var query = new GetVipIncompleteContactsQuery();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().HaveCount(1);
        result.First().FirstName.Should().Be("VipIncomplete");
    }
}
