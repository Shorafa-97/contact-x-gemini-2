using System.Text.Json;

namespace ContactsX.Application.DTOs.AuditLog;

public record AuditLogDto(
    Guid Id,
    string EntityType,
    Guid EntityId,
    string Action,
    string Changes,
    Guid? PerformedBy,
    DateTime CreatedAt
);
