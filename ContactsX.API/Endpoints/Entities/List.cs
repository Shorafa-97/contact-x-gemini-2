using FastEndpoints;
using MediatR;
using ContactsX.Application.DTOs.Entity;
using ContactsX.Application.Features.Entities.Queries;

namespace ContactsX.API.Endpoints.Entities;

public class ListEntitiesEndpoint : EndpointWithoutRequest<IEnumerable<EntityDto>>
{
    private readonly IMediator _mediator;

    public ListEntitiesEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get("");
        Group<EntityGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetEntitiesQuery(), ct);
        await HttpContext.Response.SendAsync(result, cancellation: ct);
    }
}
