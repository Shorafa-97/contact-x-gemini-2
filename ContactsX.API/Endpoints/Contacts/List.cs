using FastEndpoints;
using MediatR;
using ContactsX.Application.DTOs.Contact;
using ContactsX.Application.Features.Contacts.Queries;

namespace ContactsX.API.Endpoints.Contacts;

public class ListContactsEndpoint : EndpointWithoutRequest<IEnumerable<ContactDto>>
{
    private readonly IMediator _mediator;

    public ListContactsEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get("");
        Group<ContactGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetContactsQuery(), ct);
        await HttpContext.Response.SendAsync(result, 200, cancellation: ct);
    }
}
