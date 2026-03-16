using FastEndpoints;
using MediatR;
using ContactsX.Application.Features.Duplicates.Commands;

namespace ContactsX.API.Endpoints.Duplicates;

public class DismissDuplicateEndpoint : EndpointWithoutRequest
{
    private readonly IMediator _mediator;

    public DismissDuplicateEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Post("{id:guid}/dismiss");
        Group<DuplicateGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<Guid>("id");
        var success = await _mediator.Send(new DismissDuplicateCommand(id), ct);

        if (!success)
        {
            await HttpContext.Response.SendNotFoundAsync(ct);
            return;
        }

        await HttpContext.Response.SendOkAsync(ct);
    }
}
