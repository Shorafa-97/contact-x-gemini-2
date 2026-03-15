using FastEndpoints;
using MediatR;
using ContactsX.Application.DTOs.Relation;
using ContactsX.Application.Features.Contacts.Queries;

namespace ContactsX.API.Endpoints.Contacts;

public class ListRelationsEndpoint : EndpointWithoutRequest<IEnumerable<RelationWithEntityDto>>
{
    private readonly IMediator _mediator;

    public ListRelationsEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get("{id}/relations");
        Group<ContactGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var contactId = Route<Guid>("id");
        var result = await _mediator.Send(new GetContactRelationsQuery(contactId), ct);
        await HttpContext.Response.SendAsync(result, 200, cancellation: ct);
    }
}
