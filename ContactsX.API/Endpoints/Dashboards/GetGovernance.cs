using FastEndpoints;
using MediatR;
using ContactsX.Application.DTOs.Dashboard;
using ContactsX.Application.Features.Dashboards.Queries;

namespace ContactsX.API.Endpoints.Dashboards;

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
