using ContactsX.Application.DTOs.Entity;
using ContactsX.Application.Features.Entities.Queries;
using ContactsX.Domain.Entities;
using ContactsX.Application.Interfaces.Repositories;
using ContactsX.Application.DTOs.Shared;
using MediatR;

using System.Text.Json;

namespace ContactsX.Application.Features.Entities.Handlers;

public class GetEntityChildrenHandler : IRequestHandler<GetEntityChildrenQuery, IEnumerable<EntityDto>?>
{
    private readonly IRepository<Entity> _repository;

    public GetEntityChildrenHandler(IRepository<Entity> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<EntityDto>?> Handle(GetEntityChildrenQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id);
        if (entity == null) return null;

        var children = await _repository.FindAsync(e => e.ParentEntityId == request.Id);
        return children.Select(e => new EntityDto(
            e.Id,
            e.NameEn,
            e.NameAr,
            e.Type.ToString(),
            e.Country,
            e.Sector,
            e.RegistrationId,
            e.ParentEntityId,
            JsonSerializer.Deserialize<List<AddressDto>>(e.Addresses),
            JsonSerializer.Deserialize<List<ContactPointDto>>(e.ContactPoints),
            e.ProfileCompleteness,
            e.IsActive,
            e.CreatedAt,
            e.UpdatedAt ?? e.CreatedAt
        ));
    }
}
