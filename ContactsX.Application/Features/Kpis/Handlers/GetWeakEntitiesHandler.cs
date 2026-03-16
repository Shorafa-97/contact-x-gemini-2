using MediatR;
using ContactsX.Application.DTOs.Entity;
using ContactsX.Application.Features.Kpis.Queries;
using ContactsX.Application.Interfaces.Repositories;
using ContactsX.Domain.Entities;

namespace ContactsX.Application.Features.Kpis.Handlers;

public class GetWeakEntitiesHandler : IRequestHandler<GetWeakEntitiesQuery, IEnumerable<EntityDto>>
{
    private readonly IRepository<Entity> _entityRepo;

    public GetWeakEntitiesHandler(IRepository<Entity> entityRepo)
    {
        _entityRepo = entityRepo;
    }

    public async Task<IEnumerable<EntityDto>> Handle(GetWeakEntitiesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _entityRepo.FindAsync(e => e.ProfileCompleteness < 50);
        
        return entities.Take(request.Limit).Select(e => new EntityDto(
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
