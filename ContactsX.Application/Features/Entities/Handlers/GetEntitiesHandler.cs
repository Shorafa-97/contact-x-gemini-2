using ContactsX.Application.DTOs.Entity;
using ContactsX.Application.Features.Entities.Queries;
using ContactsX.Domain.Entities;
using ContactsX.Application.Interfaces.Repositories;
using ContactsX.Application.DTOs.Shared;
using MediatR;

using System.Text.Json;

namespace ContactsX.Application.Features.Entities.Handlers;

public class GetEntitiesHandler : IRequestHandler<GetEntitiesQuery, IEnumerable<EntityDto>>
{
    private readonly IRepository<Entity> _repository;

    public GetEntitiesHandler(IRepository<Entity> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<EntityDto>> Handle(GetEntitiesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync();
        return entities.Select(e => new EntityDto(
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
