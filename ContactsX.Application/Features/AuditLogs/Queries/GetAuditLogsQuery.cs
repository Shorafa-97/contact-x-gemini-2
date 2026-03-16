using MediatR;
using ContactsX.Application.DTOs.AuditLog;

namespace ContactsX.Application.Features.AuditLogs.Queries;

public record GetAuditLogsQuery(
    string? EntityType = null,
    string? Action = null,
    int Limit = 200
) : IRequest<IEnumerable<AuditLogDto>>;
