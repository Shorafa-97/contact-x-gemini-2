using MediatR;
using ContactsX.Application.Features.Kpis.Queries;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace ContactsX.API.Endpoints.Kpis;

public static class GetOrphanEntitiesEndpoint
{
    public static void MapGetOrphanEntities(this RouteGroupBuilder group)
    {
        group.MapGet("/orphan-entities", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new GetOrphanEntitiesQuery());
            return Results.Ok(result);
        });
    }
}
