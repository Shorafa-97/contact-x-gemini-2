using ContactsX.Application.DTOs.Entity;
using ContactsX.Application.Features.Entities.Queries;
using ContactsX.Domain.Entities;
using ContactsX.Application.Interfaces.Repositories;
using ContactsX.Application.DTOs.Shared;
using MediatR;

using System.Text.Json;

namespace ContactsX.Application.Features.Entities.Handlers;

public class GetEntityByIdHandler : IRequestHandler<GetEntityByIdQuery, EntityDto?>
{
    private readonly IRepository<Entity> _repository;

    public GetEntityByIdHandler(IRepository<Entity> repository)
    {
        _repository = repository;
    }

    public async Task<EntityDto?> Handle(GetEntityByIdQuery request, CancellationToken cancellationToken)
    {
        var e = await _repository.GetByIdAsync(request.Id);
        if (e == null) return null;

        return new EntityDto(
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
        );
    }
}
