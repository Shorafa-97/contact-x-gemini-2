using ContactsX.Application.Features.Contacts.Commands;
using ContactsX.Domain.Entities;
using ContactsX.Application.Interfaces.Repositories;
using MediatR;
using System.Text.Json;

namespace ContactsX.Application.Features.Contacts.Handlers;

public class CreateContactHandler : IRequestHandler<CreateContactCommand, Guid>
{
    private readonly IRepository<Contact> _repository;

    public CreateContactHandler(IRepository<Contact> repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateContactCommand request, CancellationToken cancellationToken)
    {
        var dto = request.ContactDto;
        var contact = new Contact
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Prefix = dto.Prefix,
            MiddleName = dto.MiddleName,
            Suffix = dto.Suffix,
            PrefixAr = dto.PrefixAr,
            FirstNameAr = dto.FirstNameAr,
            MiddleNameAr = dto.MiddleNameAr,
            LastNameAr = dto.LastNameAr,
            SuffixAr = dto.SuffixAr,
            Gender = dto.Gender,
            DateOfBirth = dto.DateOfBirth,
            Nationality = dto.Nationality,
            NationalId = dto.NationalId,
            ContactType = dto.ContactType,
            CurrentPosition = dto.CurrentPosition,
            CurrentEntityId = dto.CurrentEntityId,
            CurrentEntityName = dto.CurrentEntityName,
            Emails = JsonSerializer.Serialize(dto.Emails ?? []),
            Phones = JsonSerializer.Serialize(dto.Phones ?? []),
            Addresses = JsonSerializer.Serialize(dto.Addresses ?? []),
            Classifications = JsonSerializer.Serialize(dto.Classifications ?? []),
            CreatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(contact);
        await _repository.SaveChangesAsync();

        return contact.Id;
    }
}
