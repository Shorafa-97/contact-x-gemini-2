namespace ContactsX.Application.DTOs.Duplicate;

public record DuplicateCandidateDto(
    Guid Id,
    string EntityType,
    Guid Record1Id,
    Guid Record2Id,
    int MatchScore,
    List<string>? MatchReasons,
    string Status,
    DateTime CreatedAt,
    DateTime? ResolvedAt,
    Guid? ResolvedBy,
    string? Record1Snapshot = null,
    string? Record2Snapshot = null);
