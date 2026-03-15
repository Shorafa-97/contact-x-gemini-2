using FastEndpoints;
using MediatR;
using ContactsX.Application.DTOs.Contact;
using ContactsX.Application.Features.Contacts.Commands;

namespace ContactsX.API.Endpoints.Contacts;

public class CreateContactEndpoint : Endpoint<CreateContactDto, object>
{
    private readonly IMediator _mediator;

    public CreateContactEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Post("");
        Group<ContactGroup>();
    }

    public override async Task HandleAsync(CreateContactDto req, CancellationToken ct)
    {
        var id = await _mediator.Send(new CreateContactCommand(req), ct);
        await HttpContext.Response.SendAsync(new { id }, 201, cancellation: ct);
    }
}
