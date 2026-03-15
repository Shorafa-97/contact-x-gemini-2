using FastEndpoints;
using MediatR;
using ContactsX.Application.Features.Contacts.Commands;

namespace ContactsX.API.Endpoints.Contacts;

public class DeleteContactEndpoint : EndpointWithoutRequest
{
    private readonly IMediator _mediator;

    public DeleteContactEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Delete("{id}");
        Group<ContactGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<Guid>("id");
        var success = await _mediator.Send(new DeleteContactCommand(id), ct);

        if (!success)
        {
            await HttpContext.Response.SendNotFoundAsync(ct);
            return;
        }

        await HttpContext.Response.SendOkAsync(ct);
    }
}
