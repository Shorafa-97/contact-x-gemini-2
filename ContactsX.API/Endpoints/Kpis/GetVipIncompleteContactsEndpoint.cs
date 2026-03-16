using MediatR;
using ContactsX.Application.Features.Kpis.Queries;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace ContactsX.API.Endpoints.Kpis;

public static class GetVipIncompleteContactsEndpoint
{
    public static void MapGetVipIncompleteContacts(this RouteGroupBuilder group)
    {
        group.MapGet("/vip-incomplete", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new GetVipIncompleteContactsQuery());
            return Results.Ok(result);
        });
    }
}
