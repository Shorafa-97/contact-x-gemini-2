using FastEndpoints;
using MediatR;
using ContactsX.Application.DTOs.Relation;
using ContactsX.Application.Features.Contacts.Commands;

namespace ContactsX.API.Endpoints.Contacts;

public class CreateRelationEndpoint : Endpoint<CreateRelationDto, RelationDto>
{
    private readonly IMediator _mediator;

    public CreateRelationEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Post("{id}/relations");
        Group<ContactGroup>();
    }

    public override async Task HandleAsync(CreateRelationDto req, CancellationToken ct)
    {
        var contactId = Route<Guid>("id");
        var result = await _mediator.Send(new CreateRelationCommand(contactId, req), ct);

        if (result == null)
        {
            await HttpContext.Response.SendNotFoundAsync(ct);
            return;
        }

        await HttpContext.Response.SendAsync(result, 200, cancellation: ct);
    }
}
