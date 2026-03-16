using FastEndpoints;
using MediatR;
using ContactsX.Application.Features.Kpis.Queries;
using ContactsX.Application.DTOs.Contact;

namespace ContactsX.API.Endpoints.Kpis;

public class GetVipIncompleteContactsEndpoint : EndpointWithoutRequest<IEnumerable<ContactDto>>
{
    private readonly IMediator _mediator;

    public GetVipIncompleteContactsEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get("vip-incomplete");
        Group<KpiGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetVipIncompleteContactsQuery(), ct);
        await HttpContext.Response.SendAsync(result, cancellation: ct);
    }
}
