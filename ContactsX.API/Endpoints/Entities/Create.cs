using FastEndpoints;
using MediatR;
using ContactsX.Application.DTOs.Entity;
using ContactsX.Application.Features.Entities.Commands;

namespace ContactsX.API.Endpoints.Entities;

public class CreateEntityEndpoint : Endpoint<CreateEntityDto, object>
{
    private readonly IMediator _mediator;

    public CreateEntityEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Post("");
        Group<EntityGroup>();
    }

    public override async Task HandleAsync(CreateEntityDto req, CancellationToken ct)
    {
        var id = await _mediator.Send(new CreateEntityCommand(req), ct);
        await HttpContext.Response.SendAsync(new { id }, 201, cancellation: ct);
    }
}
