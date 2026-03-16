using MediatR;
using ContactsX.Application.DTOs.Contact;
using ContactsX.Application.Features.Kpis.Queries;
using ContactsX.Application.Interfaces.Repositories;
using ContactsX.Domain.Entities;
using ContactsX.Application.DTOs.Shared;
using System.Text.Json;

namespace ContactsX.Application.Features.Kpis.Handlers;

public class GetOrphanContactsHandler : IRequestHandler<GetOrphanContactsQuery, IEnumerable<ContactDto>>
{
    private readonly IRepository<Contact> _contactRepo;
    private readonly IRepository<Relation> _relationRepo;

    public GetOrphanContactsHandler(IRepository<Contact> contactRepo, IRepository<Relation> relationRepo)
    {
        _contactRepo = contactRepo;
        _relationRepo = relationRepo;
    }

    public async Task<IEnumerable<ContactDto>> Handle(GetOrphanContactsQuery request, CancellationToken cancellationToken)
    {
        var allContacts = await _contactRepo.GetAllAsync();
        var allRelations = await _relationRepo.GetAllAsync();
        
        var orphanContacts = allContacts.Where(c => !allRelations.Any(r => r.ContactId == c.Id));
        
        return orphanContacts.Select(c => new ContactDto
        {
            Id = c.Id,
            FirstName = c.FirstName,
            LastName = c.LastName,
            ProfileCompleteness = c.ProfileCompleteness,
            IsActive = c.IsActive,
            ContactType = c.ContactType,
            Emails = JsonSerializer.Deserialize<List<string>>(c.Emails)?.Select(e => new EmailDto { Value = e }).ToList(),
            Phones = JsonSerializer.Deserialize<List<string>>(c.Phones)?.Select(p => new PhoneDto { Value = p }).ToList(),
            Classifications = JsonSerializer.Deserialize<List<string>>(c.Classifications)
        });
    }
}
