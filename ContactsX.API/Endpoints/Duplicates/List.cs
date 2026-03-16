using FastEndpoints;
using MediatR;
using ContactsX.Application.DTOs.Duplicate;
using ContactsX.Application.Features.Duplicates.Queries;

namespace ContactsX.API.Endpoints.Duplicates;

public class ListDuplicatesEndpoint : EndpointWithoutRequest<IEnumerable<DuplicateCandidateDto>>
{
    private readonly IMediator _mediator;

    public ListDuplicatesEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get("");
        Group<DuplicateGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetDuplicatesQuery(), ct);
        await HttpContext.Response.SendAsync(result, cancellation: ct);
    }
}
