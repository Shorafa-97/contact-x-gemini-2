using ContactsX.Application.DTOs.Contact;
using ContactsX.Application.DTOs.Shared;
using ContactsX.Application.Features.Contacts.Queries;
using ContactsX.Application.Interfaces.Repositories;
using ContactsX.Domain.Entities;
using MediatR;
using System.Text.Json;

namespace ContactsX.Application.Features.Contacts.Handlers;

public class GetContactByIdHandler : IRequestHandler<GetContactByIdQuery, ContactDto?>
{
    private readonly IRepository<Contact> _repository;

    public GetContactByIdHandler(IRepository<Contact> repository)
    {
        _repository = repository;
    }

    public async Task<ContactDto?> Handle(GetContactByIdQuery request, CancellationToken cancellationToken)
    {
        var contact = await _repository.GetByIdAsync(request.Id);
        if (contact == null) return null;

        return new ContactDto
        {
            Id = contact.Id,
            FirstName = contact.FirstName,
            LastName = contact.LastName,
            Prefix = contact.Prefix,
            MiddleName = contact.MiddleName,
            Suffix = contact.Suffix,
            PrefixAr = contact.PrefixAr,
            FirstNameAr = contact.FirstNameAr,
            MiddleNameAr = contact.MiddleNameAr,
            LastNameAr = contact.LastNameAr,
            SuffixAr = contact.SuffixAr,
            Gender = contact.Gender,
            DateOfBirth = contact.DateOfBirth,
            Nationality = contact.Nationality,
            NationalId = contact.NationalId,
            ContactType = contact.ContactType,
            CurrentPosition = contact.CurrentPosition,
            CurrentEntityId = contact.CurrentEntityId,
            CurrentEntityName = contact.CurrentEntityName,
            Emails = JsonSerializer.Deserialize<List<EmailDto>>(contact.Emails),
            Phones = JsonSerializer.Deserialize<List<PhoneDto>>(contact.Phones),
            Addresses = JsonSerializer.Deserialize<List<AddressDto>>(contact.Addresses),
            Classifications = JsonSerializer.Deserialize<List<string>>(contact.Classifications),
            ProfileCompleteness = contact.ProfileCompleteness,
            IsActive = contact.IsActive,
            CreatedAt = contact.CreatedAt,
            UpdatedAt = contact.UpdatedAt
        };
    }
}
