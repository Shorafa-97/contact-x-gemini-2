using FastEndpoints;
using MediatR;
using ContactsX.Application.DTOs.Duplicate;
using ContactsX.Application.Features.Duplicates.Queries;

namespace ContactsX.API.Endpoints.Duplicates;

public class GetDuplicateMetricsEndpoint : EndpointWithoutRequest<DuplicateMetricsDto>
{
    private readonly IMediator _mediator;

    public GetDuplicateMetricsEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get("kpis");
        Group<DuplicateGroup>();
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetDuplicateMetricsQuery(), ct);
        await HttpContext.Response.SendAsync(result, cancellation: ct);
    }
}
