using MediatR;
using ContactsX.Application.DTOs.Import;
using ContactsX.Application.Features.Import.Commands;
using ContactsX.Application.Interfaces.Repositories;
using ContactsX.Domain.Entities;
using System.Text.Json;

namespace ContactsX.Application.Features.Import.Handlers;

public class ImportEntitiesHandler : IRequestHandler<ImportEntitiesCommand, ImportResultDto>
{
    private readonly IRepository<Entity> _entityRepository;

    public ImportEntitiesHandler(IRepository<Entity> entityRepository)
    {
        _entityRepository = entityRepository;
    }

    public async Task<ImportResultDto> Handle(ImportEntitiesCommand request, CancellationToken cancellationToken)
    {
        int importedCount = 0;
        var errors = new List<string>();

        foreach (var record in request.Records)
        {
            try
            {
                if (!record.TryGetProperty("nameEn", out var nameEnProp) || nameEnProp.ValueKind == JsonValueKind.Null)
                    throw new Exception("NameEn is required");

                var entity = new Entity
                {
                    NameEn = nameEnProp.GetString()!,
                    Type = record.TryGetProperty("type", out var type) ? (ContactsX.Domain.ValueOpjects.EntityType)Enum.Parse(typeof(ContactsX.Domain.ValueOpjects.EntityType), type.GetString() ?? "Public") : ContactsX.Domain.ValueOpjects.EntityType.Public,
                    Country = record.TryGetProperty("country", out var country) ? country.GetString() : null,
                    IsActive = true
                };

                await _entityRepository.AddAsync(entity);
                importedCount++;
            }
            catch (Exception ex)
            {
                errors.Add($"Error importing entity at record {importedCount + errors.Count + 1}: {ex.Message}");
            }
        }

        if (importedCount > 0)
        {
            await _entityRepository.SaveChangesAsync();
        }

        return new ImportResultDto(importedCount, errors);
    }
}
