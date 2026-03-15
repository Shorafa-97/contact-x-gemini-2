using ContactsX.Application.DTOs.Relation;
using ContactsX.Application.DTOs.Entity;
using ContactsX.Application.DTOs.Shared;
using ContactsX.Application.Features.Contacts.Queries;
using ContactsX.Application.Interfaces.Repositories;
using ContactsX.Domain.Entities;
using MediatR;
using System.Text.Json;

namespace ContactsX.Application.Features.Contacts.Handlers;

public class GetContactRelationsHandler : IRequestHandler<GetContactRelationsQuery, IEnumerable<RelationWithEntityDto>>
{
    private readonly IRepository<Relation> _relationRepository;
    private readonly IRepository<Entity> _entityRepository;

    public GetContactRelationsHandler(IRepository<Relation> relationRepository, IRepository<Entity> entityRepository)
    {
        _relationRepository = relationRepository;
        _entityRepository = entityRepository;
    }

    public async Task<IEnumerable<RelationWithEntityDto>> Handle(GetContactRelationsQuery request, CancellationToken cancellationToken)
    {
        var relations = await _relationRepository.FindAsync(r => r.ContactId == request.ContactId);
        var result = new List<RelationWithEntityDto>();

        foreach (var rel in relations)
        {
            var entity = await _entityRepository.GetByIdAsync(rel.EntityId);
            result.Add(new RelationWithEntityDto
            {
                Id = rel.Id,
                ContactId = rel.ContactId,
                EntityId = rel.EntityId,
                Role = rel.Role,
                IsPrimary = rel.IsPrimary,
                IsActive = rel.IsActive,
                StartDate = rel.StartDate,
                EndDate = rel.EndDate,
                CreatedAt = rel.CreatedAt,
                Entity = entity == null ? null : new EntityDto
                {
                    Id = entity.Id,
                    NameEn = entity.NameEn,
                    NameAr = entity.NameAr,
                    Type = entity.Type,
                    Country = entity.Country,
                    Sector = entity.Sector,
                    RegistrationId = entity.RegistrationId,
                    ParentEntityId = entity.ParentEntityId,
                    Addresses = JsonSerializer.Deserialize<List<AddressDto>>(entity.Addresses),
                    ContactPoints = JsonSerializer.Deserialize<List<ContactPointDto>>(entity.ContactPoints),
                    ProfileCompleteness = entity.ProfileCompleteness,
                    IsActive = entity.IsActive
                }
            });
        }

        return result;
    }
}
