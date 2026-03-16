using MediatR;
using ContactsX.Application.Features.Kpis.Queries;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;

namespace ContactsX.API.Endpoints.Kpis;

public static class GetWeakContactsEndpoint
{
    public static void MapGetWeakContacts(this RouteGroupBuilder group)
    {
        group.MapGet("/weak-contacts", async ([FromQuery] int limit, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetWeakContactsQuery(limit > 0 ? limit : 50));
            return Results.Ok(result);
        });
    }
}
