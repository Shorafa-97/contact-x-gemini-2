using MediatR;
using ContactsX.Application.Features.Kpis.Queries;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace ContactsX.API.Endpoints.Kpis;

public static class GetOrphanContactsEndpoint
{
    public static void MapGetOrphanContacts(this RouteGroupBuilder group)
    {
        group.MapGet("/orphan-contacts", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new GetOrphanContactsQuery());
            return Results.Ok(result);
        });
    }
}
