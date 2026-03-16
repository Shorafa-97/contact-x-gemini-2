using FastEndpoints;
using MediatR;
using ContactsX.Application.Features.Kpis.Queries;
using ContactsX.Application.DTOs.Entity;

namespace ContactsX.API.Endpoints.Kpis;

public class GetWeakEntitiesEndpoint : Endpoint<GetWeakEntitiesQuery, IEnumerable<EntityDto>>
{
    private readonly IMediator _mediator;

    public GetWeakEntitiesEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get("weak-entities");
        Group<KpiGroup>();
    }

    public override async Task HandleAsync(GetWeakEntitiesQuery req, CancellationToken ct)
    {
        var result = await _mediator.Send(req, ct);
        await HttpContext.Response.SendAsync(result, cancellation: ct);
    }
}
