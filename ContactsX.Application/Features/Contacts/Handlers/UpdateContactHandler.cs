using ContactsX.Application.Features.Contacts.Commands;
using ContactsX.Domain.Entities;
using ContactsX.Application.Interfaces.Repositories;
using MediatR;
using System.Text.Json;

namespace ContactsX.Application.Features.Contacts.Handlers;

public class UpdateContactHandler : IRequestHandler<UpdateContactCommand, bool>
{
    private readonly IRepository<Contact> _repository;

    public UpdateContactHandler(IRepository<Contact> repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
    {
        var contact = await _repository.GetByIdAsync(request.Id);
        if (contact == null) return false;

        var dto = request.ContactDto;

        if (dto.FirstName != null) contact.FirstName = dto.FirstName;
        if (dto.LastName != null) contact.LastName = dto.LastName;
        if (dto.Prefix != null) contact.Prefix = dto.Prefix;
        if (dto.MiddleName != null) contact.MiddleName = dto.MiddleName;
        if (dto.Suffix != null) contact.Suffix = dto.Suffix;
        if (dto.PrefixAr != null) contact.PrefixAr = dto.PrefixAr;
        if (dto.FirstNameAr != null) contact.FirstNameAr = dto.FirstNameAr;
        if (dto.MiddleNameAr != null) contact.MiddleNameAr = dto.MiddleNameAr;
        if (dto.LastNameAr != null) contact.LastNameAr = dto.LastNameAr;
        if (dto.SuffixAr != null) contact.SuffixAr = dto.SuffixAr;
        if (dto.Gender.HasValue) contact.Gender = dto.Gender;
        if (dto.DateOfBirth != null) contact.DateOfBirth = dto.DateOfBirth;
        if (dto.Nationality != null) contact.Nationality = dto.Nationality;
        if (dto.NationalId != null) contact.NationalId = dto.NationalId;
        if (dto.ContactType.HasValue) contact.ContactType = dto.ContactType.Value;
        if (dto.CurrentPosition != null) contact.CurrentPosition = dto.CurrentPosition;
        if (dto.CurrentEntityId.HasValue) contact.CurrentEntityId = dto.CurrentEntityId;
        if (dto.CurrentEntityName != null) contact.CurrentEntityName = dto.CurrentEntityName;
        if (dto.Emails != null) contact.Emails = JsonSerializer.Serialize(dto.Emails);
        if (dto.Phones != null) contact.Phones = JsonSerializer.Serialize(dto.Phones);
        if (dto.Addresses != null) contact.Addresses = JsonSerializer.Serialize(dto.Addresses);
        if (dto.Classifications != null) contact.Classifications = JsonSerializer.Serialize(dto.Classifications);
        if (dto.IsActive.HasValue) contact.IsActive = dto.IsActive.Value;

        contact.UpdatedAt = DateTime.UtcNow;

        _repository.Update(contact);
        await _repository.SaveChangesAsync();
        return true;
    }
}
