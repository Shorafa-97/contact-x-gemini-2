using MediatR;
using ContactsX.Application.DTOs.Contact;
using ContactsX.Application.Features.Kpis.Queries;
using ContactsX.Application.Interfaces.Repositories;
using ContactsX.Domain.Entities;
using ContactsX.Application.DTOs.Shared;
using System.Text.Json;

namespace ContactsX.Application.Features.Kpis.Handlers;

public class GetVipIncompleteContactsHandler : IRequestHandler<GetVipIncompleteContactsQuery, IEnumerable<ContactDto>>
{
    private readonly IRepository<Contact> _contactRepo;

    public GetVipIncompleteContactsHandler(IRepository<Contact> contactRepo)
    {
        _contactRepo = contactRepo;
    }

    public async Task<IEnumerable<ContactDto>> Handle(GetVipIncompleteContactsQuery request, CancellationToken cancellationToken)
    {
        var allContacts = await _contactRepo.GetAllAsync();
        
        var vipIncomplete = allContacts.Where(c => 
        {
            var classifications = JsonSerializer.Deserialize<List<string>>(c.Classifications) ?? new List<string>();
            return classifications.Contains("VIP") && c.ProfileCompleteness < 100;
        });
        
        return vipIncomplete.Select(c => new ContactDto
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
