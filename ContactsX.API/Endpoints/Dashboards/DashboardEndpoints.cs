using FastEndpoints;
using MediatR;
using ContactsX.Application.DTOs.Dashboard;
using ContactsX.Application.Features.Dashboards.Queries;

namespace ContactsX.API.Endpoints.Dashboards;

public class GetStatsEndpoint : EndpointWithoutRequest<DashboardStats>
{
    private readonly IMediator _mediator;
    public GetStatsEndpoint(IMediator mediator) => _mediator = mediator;

    public override void Configure()
    {
        Get("stats");
        Group<DashboardGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetDashboardStatsQuery(), ct);
        await HttpContext.Response.SendAsync(result, cancellation: ct);
    }
}

public class GetExecutiveEndpoint : EndpointWithoutRequest<ExecutiveDashboardData>
{
    private readonly IMediator _mediator;
    public GetExecutiveEndpoint(IMediator mediator) => _mediator = mediator;

    public override void Configure()
    {
        Get("executive");
        Group<DashboardGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetExecutiveDashboardQuery(), ct);
        await HttpContext.Response.SendAsync(result, cancellation: ct);
    }
}

public class GetGovernanceEndpoint : EndpointWithoutRequest<GovernanceDashboardData>
{
    private readonly IMediator _mediator;
    public GetGovernanceEndpoint(IMediator mediator) => _mediator = mediator;

    public override void Configure()
    {
        Get("governance");
        Group<DashboardGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetGovernanceDashboardQuery(), ct);
        await HttpContext.Response.SendAsync(result, cancellation: ct);
    }
}

public class GetOperationalEndpoint : EndpointWithoutRequest<OperationalDashboardData>
{
    private readonly IMediator _mediator;
    public GetOperationalEndpoint(IMediator mediator) => _mediator = mediator;

    public override void Configure()
    {
        Get("operational");
        Group<DashboardGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetOperationalDashboardQuery(), ct);
        await HttpContext.Response.SendAsync(result, cancellation: ct);
    }
}

public class GetKpiDuplicatesEndpoint : EndpointWithoutRequest<DuplicateMetrics>
{
    private readonly IMediator _mediator;
    public GetKpiDuplicatesEndpoint(IMediator mediator) => _mediator = mediator;

    public override void Configure()
    {
        Get("kpis/duplicates");
        Group<DashboardGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetDuplicateMetricsQuery(), ct);
        await HttpContext.Response.SendAsync(result, cancellation: ct);
    }
}
