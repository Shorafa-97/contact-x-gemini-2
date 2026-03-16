using MediatR;
using ContactsX.Application.DTOs.Entity;
using ContactsX.Application.Features.Kpis.Queries;
using ContactsX.Application.Interfaces.Repositories;
using ContactsX.Domain.Entities;

namespace ContactsX.Application.Features.Kpis.Handlers;

public class GetOrphanEntitiesHandler : IRequestHandler<GetOrphanEntitiesQuery, IEnumerable<EntityDto>>
{
    private readonly IRepository<Entity> _entityRepo;
    private readonly IRepository<Relation> _relationRepo;

    public GetOrphanEntitiesHandler(IRepository<Entity> entityRepo, IRepository<Relation> relationRepo)
    {
        _entityRepo = entityRepo;
        _relationRepo = relationRepo;
    }

    public async Task<IEnumerable<EntityDto>> Handle(GetOrphanEntitiesQuery request, CancellationToken cancellationToken)
    {
        var allEntities = await _entityRepo.GetAllAsync();
        var allRelations = await _relationRepo.GetAllAsync();
        
        var orphanEntities = allEntities.Where(e => !allRelations.Any(r => r.EntityId == e.Id));
        
        return orphanEntities.Select(e => new EntityDto(
            e.Id,
            e.NameEn,
            e.NameAr,
            e.Type.ToString(),
            e.Country,
            e.Sector,
            e.RegistrationId,
            e.ParentEntityId,
            new List<ContactsX.Application.DTOs.Shared.AddressDto>(), // Placeholder
            new List<ContactsX.Application.DTOs.Shared.ContactPointDto>(), // Placeholder
            e.ProfileCompleteness,
            e.IsActive,
            e.CreatedAt,
            DateTime.UtcNow
        ));
    }
}
