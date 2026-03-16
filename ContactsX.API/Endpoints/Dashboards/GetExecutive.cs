using FastEndpoints;
using MediatR;
using ContactsX.Application.DTOs.Dashboard;
using ContactsX.Application.Features.Dashboards.Queries;

namespace ContactsX.API.Endpoints.Dashboards;

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
