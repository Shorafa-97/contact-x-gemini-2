namespace ContactsX.Application.DTOs.Dashboard;

public record TypeCountDto(string Type, int Count);

public record DashboardStats(
    int TotalContacts,
    int TotalEntities,
    int ActiveContacts,
    int ActiveEntities,
    int AverageCompleteness,
    int DuplicateCandidates,
    int RecentActivity,
    List<TypeCountDto> ContactsByType,
    List<TypeCountDto> EntitiesByType);

public record ExecutiveDashboardData();
public record GovernanceDashboardData();
public record OperationalDashboardData();
public record DuplicateMetrics();
