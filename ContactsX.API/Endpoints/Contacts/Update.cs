using FastEndpoints;
using MediatR;
using ContactsX.Application.DTOs.Contact;
using ContactsX.Application.Features.Contacts.Commands;

namespace ContactsX.API.Endpoints.Contacts;

public class UpdateContactEndpoint : Endpoint<UpdateContactDto, object>
{
    private readonly IMediator _mediator;

    public UpdateContactEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Patch("{id}");
        Group<ContactGroup>();
    }

    public override async Task HandleAsync(UpdateContactDto req, CancellationToken ct)
    {
        var id = Route<Guid>("id");
        var success = await _mediator.Send(new UpdateContactCommand(id, req), ct);

        if (!success)
        {
            await HttpContext.Response.SendNotFoundAsync(ct);
            return;
        }

        await HttpContext.Response.SendNoContentAsync(ct);
    }
}
