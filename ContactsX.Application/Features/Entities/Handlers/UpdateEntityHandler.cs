using ContactsX.Application.Features.Entities.Commands;
using ContactsX.Domain.Entities;
using ContactsX.Application.Interfaces.Repositories;
using MediatR;
using System.Text.Json;

namespace ContactsX.Application.Features.Entities.Handlers;

public class UpdateEntityHandler : IRequestHandler<UpdateEntityCommand, bool>
{
    private readonly IRepository<Entity> _repository;

    public UpdateEntityHandler(IRepository<Entity> repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(UpdateEntityCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id);
        if (entity == null) return false;

        var dto = request.EntityDto;
        entity.NameEn = dto.NameEn;
        entity.NameAr = dto.NameAr;
        entity.Type = Enum.TryParse<ContactsX.Domain.ValueOpjects.EntityType>(dto.EntityType, true, out var type) ? type : entity.Type;
        entity.Country = dto.Country;
        entity.Sector = dto.Sector;
        entity.RegistrationId = dto.RegistrationId;
        entity.ParentEntityId = dto.ParentEntityId;
        entity.Addresses = JsonSerializer.Serialize(dto.Addresses ?? []);
        entity.ContactPoints = JsonSerializer.Serialize(dto.ContactPoints ?? []);
        entity.ProfileCompleteness = dto.ProfileCompleteness;
        entity.IsActive = dto.IsActive;
        entity.UpdatedAt = DateTime.UtcNow;

        _repository.Update(entity);
        await _repository.SaveChangesAsync();

        return true;
    }
}
