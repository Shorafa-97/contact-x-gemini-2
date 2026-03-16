using FastEndpoints;
using MediatR;
using ContactsX.Application.DTOs.Dashboard;
using ContactsX.Application.Features.Dashboards.Queries;

namespace ContactsX.API.Endpoints.Dashboards;

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
