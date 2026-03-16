using MediatR;
using ContactsX.Application.DTOs.AuditLog;
using ContactsX.Application.Features.AuditLogs.Queries;
using ContactsX.Application.Interfaces.Repositories;
using ContactsX.Domain.Entities;

namespace ContactsX.Application.Features.AuditLogs.Handlers;

public class GetAuditLogsHandler : IRequestHandler<GetAuditLogsQuery, IEnumerable<AuditLogDto>>
{
    private readonly IRepository<AuditLog> _auditLogRepo;

    public GetAuditLogsHandler(IRepository<AuditLog> auditLogRepo)
    {
        _auditLogRepo = auditLogRepo;
    }

    public async Task<IEnumerable<AuditLogDto>> Handle(GetAuditLogsQuery request, CancellationToken cancellationToken)
    {
        var logs = await _auditLogRepo.GetAllAsync();

        var query = logs.AsQueryable();

        if (!string.IsNullOrEmpty(request.EntityType))
        {
            query = query.Where(l => l.EntityType.Equals(request.EntityType, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrEmpty(request.Action))
        {
            query = query.Where(l => l.Action.Equals(request.Action, StringComparison.OrdinalIgnoreCase));
        }

        return query
            .OrderByDescending(l => l.CreatedAt)
            .Take(request.Limit)
            .Select(l => new AuditLogDto(
                l.Id,
                l.EntityType,
                l.EntityId,
                l.Action,
                l.Changes,
                l.PerformedBy,
                l.CreatedAt
            ))
            .ToList();
    }
}
