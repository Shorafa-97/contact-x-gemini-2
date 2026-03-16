using MediatR;
using ContactsX.Application.DTOs.Dashboard;
using ContactsX.Application.Features.Dashboards.Queries;
using ContactsX.Application.Interfaces.Repositories;
using ContactsX.Domain.Entities;

namespace ContactsX.Application.Features.Dashboards.Handlers;

public class GetDashboardStatsHandler : IRequestHandler<GetDashboardStatsQuery, DashboardStats>
{
    private readonly IRepository<Contact> _contactRepo;
    private readonly IRepository<Entity> _entityRepo;
    private readonly IRepository<DuplicateCandidate> _duplicateRepo;

    public GetDashboardStatsHandler(
        IRepository<Contact> contactRepo,
        IRepository<Entity> entityRepo,
        IRepository<DuplicateCandidate> duplicateRepo)
    {
        _contactRepo = contactRepo;
        _entityRepo = entityRepo;
        _duplicateRepo = duplicateRepo;
    }

    public async Task<DashboardStats> Handle(GetDashboardStatsQuery request, CancellationToken cancellationToken)
    {
        var contacts = await _contactRepo.GetAllAsync();
        var entities = await _entityRepo.GetAllAsync();
        var duplicates = await _duplicateRepo.GetAllAsync();

        return new DashboardStats(
            TotalContacts: contacts.Count(),
            TotalEntities: entities.Count(),
            ActiveContacts: contacts.Count(c => c.IsActive),
            ActiveEntities: entities.Count(e => e.IsActive),
            AverageCompleteness: (int)(entities.Any() ? entities.Average(e => e.ProfileCompleteness) : 0),
            DuplicateCandidates: duplicates.Count(d => d.Status == "pending"),
            RecentActivity: 0, // Placeholder
            ContactsByType: new List<TypeCountDto>(), // Placeholder
            EntitiesByType: new List<TypeCountDto>() // Placeholder
        );
    }
}
