using FastEndpoints;
using MediatR;
using ContactsX.Application.Features.Kpis.Queries;
using ContactsX.Application.DTOs.Entity;

namespace ContactsX.API.Endpoints.Kpis;

public class GetOrphanEntitiesEndpoint : EndpointWithoutRequest<IEnumerable<EntityDto>>
{
    private readonly IMediator _mediator;

    public GetOrphanEntitiesEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get("orphan-entities");
        Group<KpiGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetOrphanEntitiesQuery(), ct);
        await HttpContext.Response.SendAsync(result, cancellation: ct);
    }
}
