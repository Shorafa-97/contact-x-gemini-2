using FastEndpoints;
using MediatR;
using ContactsX.Application.DTOs.Dashboard;
using ContactsX.Application.Features.Dashboards.Queries;

namespace ContactsX.API.Endpoints.Dashboards;

public class GetKpiDuplicatesEndpoint : EndpointWithoutRequest<DashboardDuplicateMetrics>
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
        var result = await _mediator.Send(new GetDashboardDuplicateMetricsQuery(), ct);
        await HttpContext.Response.SendAsync(result, cancellation: ct);
    }
}
