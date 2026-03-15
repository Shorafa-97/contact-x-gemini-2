using FastEndpoints;
using MediatR;
using ContactsX.Application.DTOs.Entity;
using ContactsX.Application.Features.Entities.Commands;

namespace ContactsX.API.Endpoints.Entities;

public class UpdateEntityEndpoint : Endpoint<UpdateEntityDto, object>
{
    private readonly IMediator _mediator;

    public UpdateEntityEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Patch("{id:guid}");
        Group<EntityGroup>();
    }

    public override async Task HandleAsync(UpdateEntityDto req, CancellationToken ct)
    {
        var id = Route<Guid>("id");
        var success = await _mediator.Send(new UpdateEntityCommand(id, req), ct);

        if (!success)
        {
            await HttpContext.Response.SendNotFoundAsync(ct);
            return;
        }

        await HttpContext.Response.SendOkAsync(ct);
    }
}
