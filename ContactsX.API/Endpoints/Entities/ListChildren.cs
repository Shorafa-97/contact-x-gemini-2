using FastEndpoints;
using MediatR;
using ContactsX.Application.DTOs.Entity;
using ContactsX.Application.Features.Entities.Queries;

namespace ContactsX.API.Endpoints.Entities;

public class ListEntityChildrenEndpoint : EndpointWithoutRequest<IEnumerable<EntityDto>>
{
    private readonly IMediator _mediator;

    public ListEntityChildrenEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get("{id:guid}/children");
        Group<EntityGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<Guid>("id");
        var result = await _mediator.Send(new GetEntityChildrenQuery(id), ct);

        if (result is null)
        {
            await HttpContext.Response.SendNotFoundAsync(ct);
            return;
        }

        await HttpContext.Response.SendAsync(result, cancellation: ct);
    }
}
