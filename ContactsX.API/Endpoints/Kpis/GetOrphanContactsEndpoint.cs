using FastEndpoints;
using MediatR;
using ContactsX.Application.Features.Kpis.Queries;
using ContactsX.Application.DTOs.Contact;

namespace ContactsX.API.Endpoints.Kpis;

public class GetOrphanContactsEndpoint : EndpointWithoutRequest<IEnumerable<ContactDto>>
{
    private readonly IMediator _mediator;

    public GetOrphanContactsEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get("orphan-contacts");
        Group<KpiGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetOrphanContactsQuery(), ct);
        await HttpContext.Response.SendAsync(result, cancellation: ct);
    }
}
