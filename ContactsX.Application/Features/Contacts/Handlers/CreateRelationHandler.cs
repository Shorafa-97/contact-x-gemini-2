using ContactsX.Application.DTOs.Relation;
using ContactsX.Application.Features.Contacts.Commands;
using ContactsX.Application.Interfaces.Repositories;
using ContactsX.Domain.Entities;
using MediatR;

namespace ContactsX.Application.Features.Contacts.Handlers;

public class CreateRelationHandler : IRequestHandler<CreateRelationCommand, RelationDto?>
{
    private readonly IRepository<Relation> _relationRepository;
    private readonly IRepository<Contact> _contactRepository;
    private readonly IRepository<Entity> _entityRepository;

    public CreateRelationHandler(
        IRepository<Relation> relationRepository,
        IRepository<Contact> contactRepository,
        IRepository<Entity> entityRepository)
    {
        _relationRepository = relationRepository;
        _contactRepository = contactRepository;
        _entityRepository = entityRepository;
    }

    public async Task<RelationDto?> Handle(CreateRelationCommand request, CancellationToken cancellationToken)
    {
        // 1. Verify contact exists
        var contact = await _contactRepository.GetByIdAsync(request.ContactId);
        if (contact == null) return null;

        // 2. Verify target entity exists
        var targetEntity = await _entityRepository.GetByIdAsync(request.RelationDto.TargetEntityId);
        if (targetEntity == null) return null;

        // 3. Create relation
        var relation = new Relation
        {
            ContactId = request.ContactId,
            EntityId = request.RelationDto.TargetEntityId,
            Role = request.RelationDto.Role,
            IsPrimary = request.RelationDto.IsPrimary,
            IsActive = request.RelationDto.IsActive,
            StartDate = request.RelationDto.StartDate,
            EndDate = request.RelationDto.EndDate,
            CreatedAt = DateTime.UtcNow
        };

        await _relationRepository.AddAsync(relation);
        await _relationRepository.SaveChangesAsync();

        return new RelationDto
        {
            Id = relation.Id,
            ContactId = relation.ContactId,
            EntityId = relation.EntityId,
            Role = relation.Role,
            IsPrimary = relation.IsPrimary,
            IsActive = relation.IsActive,
            StartDate = relation.StartDate,
            EndDate = relation.EndDate,
            CreatedAt = relation.CreatedAt
        };
    }
}
