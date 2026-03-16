using MediatR;
using ContactsX.Application.DTOs.Dashboard;
using ContactsX.Application.Features.Dashboards.Queries;

namespace ContactsX.Application.Features.Dashboards.Handlers;

public class GetDashboardDataHandlers : 
    IRequestHandler<GetExecutiveDashboardQuery, ExecutiveDashboardData>,
    IRequestHandler<GetGovernanceDashboardQuery, GovernanceDashboardData>,
    IRequestHandler<GetOperationalDashboardQuery, OperationalDashboardData>,
    IRequestHandler<GetDashboardDuplicateMetricsQuery, DashboardDuplicateMetrics>
{
    public Task<ExecutiveDashboardData> Handle(GetExecutiveDashboardQuery request, CancellationToken cancellationToken)
        => Task.FromResult(new ExecutiveDashboardData());

    public Task<GovernanceDashboardData> Handle(GetGovernanceDashboardQuery request, CancellationToken cancellationToken)
        => Task.FromResult(new GovernanceDashboardData());

    public Task<OperationalDashboardData> Handle(GetOperationalDashboardQuery request, CancellationToken cancellationToken)
        => Task.FromResult(new OperationalDashboardData());

    public Task<DashboardDuplicateMetrics> Handle(GetDashboardDuplicateMetricsQuery request, CancellationToken cancellationToken)
        => Task.FromResult(new DashboardDuplicateMetrics());
}
