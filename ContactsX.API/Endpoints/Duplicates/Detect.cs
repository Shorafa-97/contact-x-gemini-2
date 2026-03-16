using FastEndpoints;
using MediatR;
using ContactsX.Application.Features.Duplicates.Commands;

namespace ContactsX.API.Endpoints.Duplicates;

public class DetectDuplicatesEndpoint : EndpointWithoutRequest
{
    private readonly IMediator _mediator;

    public DetectDuplicatesEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Post("detect");
        Group<DuplicateGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await _mediator.Send(new DetectDuplicatesCommand(), ct);
        await HttpContext.Response.SendOkAsync(ct);
    }
}
