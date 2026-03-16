using MediatR;
using ContactsX.Application.Features.Import.Commands;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace ContactsX.API.Endpoints.Import;

public static class ImportEntities
{
    public static void MapImportEntities(this RouteGroupBuilder group)
    {
        group.MapPost("/entities", async (JsonElement[] records, IMediator mediator) =>
        {
            var result = await mediator.Send(new ImportEntitiesCommand(records));
            return Results.Ok(result);
        })
        .AddEndpointFilter(async (context, next) =>
        {
            var records = context.GetArgument<JsonElement[]>(0);
            if (records == null || records.Length == 0)
            {
                return Results.BadRequest(new { message = "The import list must contain at least one item." });
            }
            return await next(context);
        });
    }
}
