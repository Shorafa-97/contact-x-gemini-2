using MediatR;
using ContactsX.Application.DTOs.Dashboard;

namespace ContactsX.Application.Features.Dashboards.Queries;

public record GetDashboardStatsQuery() : IRequest<DashboardStats>;
public record GetExecutiveDashboardQuery() : IRequest<ExecutiveDashboardData>;
public record GetGovernanceDashboardQuery() : IRequest<GovernanceDashboardData>;
public record GetOperationalDashboardQuery() : IRequest<OperationalDashboardData>;
public record GetDashboardDuplicateMetricsQuery() : IRequest<DashboardDuplicateMetrics>;

