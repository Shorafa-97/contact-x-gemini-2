using ContactsX.Application.DTOs.Entity;
using ContactsX.Application.DTOs.Contact;
using ContactsX.Application.Features.Entities.Queries;
using ContactsX.Domain.Entities;
using ContactsX.Application.Interfaces.Repositories;
using ContactsX.Application.DTOs.Shared;
using MediatR;

using System.Text.Json;

namespace ContactsX.Application.Features.Entities.Handlers;

public class GetEntityContactsHandler : IRequestHandler<GetEntityContactsQuery, IEnumerable<RelationWithContactDto>?>
{
    private readonly IRepository<Entity> _entityRepository;
    private readonly IRepository<Relation> _relationRepository;

    public GetEntityContactsHandler(IRepository<Entity> entityRepository, IRepository<Relation> relationRepository)
    {
        _entityRepository = entityRepository;
        _relationRepository = relationRepository;
    }

    public async Task<IEnumerable<RelationWithContactDto>?> Handle(GetEntityContactsQuery request, CancellationToken cancellationToken)
    {
        var entity = await _entityRepository.GetByIdAsync(request.Id);
        if (entity == null) return null;

        var relations = await _relationRepository.FindAsync(r => r.EntityId == request.Id);
        
        return relations.Select(r => new RelationWithContactDto
        {
            Id = r.Id,
            ContactId = r.ContactId,
            EntityId = r.EntityId,
            Role = r.Role,
            IsPrimary = r.IsPrimary,
            IsActive = r.IsActive,
            StartDate = r.StartDate,
            EndDate = r.EndDate,
            CreatedAt = r.CreatedAt,
            Contact = r.Contact == null ? null : new ContactDto
            {
                Id = r.Contact.Id,
                FirstName = r.Contact.FirstName,
                LastName = r.Contact.LastName,
                Prefix = r.Contact.Prefix,
                MiddleName = r.Contact.MiddleName,
                Suffix = r.Contact.Suffix,
                PrefixAr = r.Contact.PrefixAr,
                FirstNameAr = r.Contact.FirstNameAr,
                MiddleNameAr = r.Contact.MiddleNameAr,
                LastNameAr = r.Contact.LastNameAr,
                SuffixAr = r.Contact.SuffixAr,
                Gender = r.Contact.Gender,
                DateOfBirth = r.Contact.DateOfBirth,
                Nationality = r.Contact.Nationality,
                NationalId = r.Contact.NationalId,
                ContactType = r.Contact.ContactType,
                CurrentPosition = r.Contact.CurrentPosition,
                CurrentEntityId = r.Contact.CurrentEntityId,
                CurrentEntityName = r.Contact.CurrentEntityName,
                ProfileCompleteness = r.Contact.ProfileCompleteness,
                IsActive = r.Contact.IsActive,
                CreatedAt = r.Contact.CreatedAt,
                UpdatedAt = r.Contact.UpdatedAt
            }
        });
    }
}
