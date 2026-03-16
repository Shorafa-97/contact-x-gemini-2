namespace ContactsX.Application.DTOs.Duplicate;

public record DuplicateMetricsDto(
    int Total,
    int HighConfidence,
    int MediumConfidence,
    int LowConfidence,
    int Pending,
    int Resolved,
    int ResolutionRate);
