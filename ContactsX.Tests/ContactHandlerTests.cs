using ContactsX.Application.DTOs.Contact;
using ContactsX.Application.Features.Contacts.Commands;
using ContactsX.Application.Features.Contacts.Handlers;
using ContactsX.Application.Features.Contacts.Queries;
using ContactsX.Domain.Entities;
using ContactsX.Domain.ValueOpjects;
using ContactsX.Infrastructure.Repositories;
using ContactsX.Persistence.DatabBaseContext;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ContactsX.Tests;

public class ContactHandlerTests
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
    public async Task CreateContactHandler_ShouldAddContactToDatabase()
    {
        // Arrange
        var context = GetDbContext();
        var repository = new Repository<Contact>(context);
        var handler = new CreateContactHandler(repository);

        var dto = new CreateContactDto
        {
            FirstName = "Jane",
            LastName = "Smith",
            ContactType = ContactType.Employee,
            Gender = Gender.Female
        };
        var command = new CreateContactCommand(dto);

        // Act
        var resultId = await handler.Handle(command, CancellationToken.None);

        // Assert
        resultId.Should().NotBeEmpty();
        var contact = await context.Contacts.FindAsync(resultId);
        contact.Should().NotBeNull();
        contact!.FirstName.Should().Be("Jane");
        contact.LastName.Should().Be("Smith");
        contact.ContactType.Should().Be(ContactType.Employee);
    }

    [Fact]
    public async Task GetContactByIdHandler_ShouldReturnCorrectContact()
    {
        // Arrange
        var context = GetDbContext();
        var repository = new Repository<Contact>(context);
        
        var contactId = Guid.NewGuid();
        var contact = new Contact
        {
            Id = contactId,
            FirstName = "Alice",
            LastName = "Wonder",
            ContactType = ContactType.Vip,
            Emails = "[]",
            Phones = "[]",
            Addresses = "[]",
            Classifications = "[]"
        };
        context.Contacts.Add(contact);
        await context.SaveChangesAsync();

        var handler = new GetContactByIdHandler(repository);
        var query = new GetContactByIdQuery(contactId);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(contactId);
        result.FirstName.Should().Be("Alice");
    }
}
