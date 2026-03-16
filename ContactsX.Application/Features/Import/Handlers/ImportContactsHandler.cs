using MediatR;
using ContactsX.Application.DTOs.Import;
using ContactsX.Application.Features.Import.Commands;
using ContactsX.Application.Interfaces.Repositories;
using ContactsX.Domain.Entities;
using System.Text.Json;

namespace ContactsX.Application.Features.Import.Handlers;

public class ImportContactsHandler : IRequestHandler<ImportContactsCommand, ImportResultDto>
{
    private readonly IRepository<Contact> _contactRepository;

    public ImportContactsHandler(IRepository<Contact> contactRepository)
    {
        _contactRepository = contactRepository;
    }

    public async Task<ImportResultDto> Handle(ImportContactsCommand request, CancellationToken cancellationToken)
    {
        int importedCount = 0;
        var errors = new List<string>();

        foreach (var record in request.Records)
        {
            try
            {
                // Basic mapping logic - extension point for flexible mapping
                if (!record.TryGetProperty("firstName", out var firstNameProp) || firstNameProp.ValueKind == JsonValueKind.Null)
                    throw new Exception("FirstName is required");
                
                if (!record.TryGetProperty("lastName", out var lastNameProp) || lastNameProp.ValueKind == JsonValueKind.Null)
                    throw new Exception("LastName is required");

                var contact = new Contact
                {
                    FirstName = firstNameProp.GetString()!,
                    LastName = lastNameProp.GetString()!,
                    Emails = record.TryGetProperty("email", out var email) ? $"[\"{email.GetString()}\"]" : "[]",
                    Phones = record.TryGetProperty("phone", out var phone) ? $"[\"{phone.GetString()}\"]" : "[]",
                    IsActive = true
                };

                await _contactRepository.AddAsync(contact);
                importedCount++;
            }
            catch (Exception ex)
            {
                errors.Add($"Error importing contact at record {importedCount + errors.Count + 1}: {ex.Message}");
            }
        }

        if (importedCount > 0)
        {
            await _contactRepository.SaveChangesAsync();
        }

        return new ImportResultDto(importedCount, errors);
    }
}
