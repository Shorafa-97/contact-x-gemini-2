using ContactsX.Application.Features.Entities.Commands;
using ContactsX.Domain.Entities;
using ContactsX.Application.Interfaces.Repositories;
using MediatR;
using System.Text.Json;

namespace ContactsX.Application.Features.Entities.Handlers;

public class CreateEntityHandler : IRequestHandler<CreateEntityCommand, Guid>
{
    private readonly IRepository<Entity> _repository;

    public CreateEntityHandler(IRepository<Entity> repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateEntityCommand request, CancellationToken cancellationToken)
    {
        var dto = request.EntityDto;
        var entity = new Entity
        {
            NameEn = dto.NameEn,
            NameAr = dto.NameAr,
            Type = Enum.TryParse<ContactsX.Domain.ValueOpjects.EntityType>(dto.EntityType, true, out var type) ? type : ContactsX.Domain.ValueOpjects.EntityType.Public,
            Country = dto.Country,
            Sector = dto.Sector,
            RegistrationId = dto.RegistrationId,
            ParentEntityId = dto.ParentEntityId,
            Addresses = JsonSerializer.Serialize(dto.Addresses ?? []),
            ContactPoints = JsonSerializer.Serialize(dto.ContactPoints ?? []),
            ProfileCompleteness = dto.ProfileCompleteness,
            IsActive = dto.IsActive,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(entity);
        await _repository.SaveChangesAsync();

        return entity.Id;
    }
}
