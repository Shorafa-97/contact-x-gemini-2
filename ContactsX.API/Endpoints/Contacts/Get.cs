using FastEndpoints;
using MediatR;
using ContactsX.Application.DTOs.Contact;
using ContactsX.Application.Features.Contacts.Queries;

namespace ContactsX.API.Endpoints.Contacts;

public class GetContactEndpoint : EndpointWithoutRequest<ContactDto>
{
    private readonly IMediator _mediator;

    public GetContactEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get("{id}");
        Group<ContactGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<Guid>("id");
        var result = await _mediator.Send(new GetContactByIdQuery(id), ct);

        if (result == null)
        {
            await HttpContext.Response.SendNotFoundAsync(ct);
            return;
        }

        await HttpContext.Response.SendAsync(result, 200, cancellation: ct);
    }
}
