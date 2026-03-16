using MediatR;
using ContactsX.Application.Features.Kpis.Queries;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;

namespace ContactsX.API.Endpoints.Kpis;

public static class GetWeakEntitiesEndpoint
{
    public static void MapGetWeakEntities(this RouteGroupBuilder group)
    {
        group.MapGet("/weak-entities", async ([FromQuery] int limit, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetWeakEntitiesQuery(limit > 0 ? limit : 50));
            return Results.Ok(result);
        });
    }
}
