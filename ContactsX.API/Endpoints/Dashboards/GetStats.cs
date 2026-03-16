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
