using FastEndpoints;
using MediatR;
using ContactsX.Application.Features.Kpis.Queries;
using ContactsX.Application.DTOs.Contact;

namespace ContactsX.API.Endpoints.Kpis;

public class GetWeakContactsEndpoint : Endpoint<GetWeakContactsQuery, IEnumerable<ContactDto>>
{
    private readonly IMediator _mediator;

    public GetWeakContactsEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get("weak-contacts");
        Group<KpiGroup>();
    }

    public override async Task HandleAsync(GetWeakContactsQuery req, CancellationToken ct)
    {
        var result = await _mediator.Send(req, ct);
        await HttpContext.Response.SendAsync(result, cancellation: ct);
    }
}
