using FastEndpoints;
using MediatR;
using ContactsX.Application.Features.Entities.Commands;

namespace ContactsX.API.Endpoints.Entities;

public class DeleteEntityEndpoint : EndpointWithoutRequest
{
    private readonly IMediator _mediator;

    public DeleteEntityEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Delete("{id:guid}");
        Group<EntityGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<Guid>("id");
        var success = await _mediator.Send(new DeleteEntityCommand(id), ct);

        if (!success)
        {
            await HttpContext.Response.SendNotFoundAsync(ct);
            return;
        }

        await HttpContext.Response.SendOkAsync(ct);
    }
}
