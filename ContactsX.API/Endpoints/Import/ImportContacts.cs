using FastEndpoints;
using MediatR;
using ContactsX.Application.Features.Import.Commands;
using ContactsX.Application.DTOs.Import;
using System.Text.Json;

namespace ContactsX.API.Endpoints.Import;

public class ImportContactsEndpoint : Endpoint<ImportContactsCommand, ImportResultDto>
{
    private readonly IMediator _mediator;

    public ImportContactsEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Post("contacts");
        Group<ImportGroup>();
        Summary(s => {
            s.Description = "Import contacts from a list of JSON objects.";
            s.Responses[200] = "Successfully imported some or all contacts.";
            s.Responses[400] = "Invalid input data.";
        });
    }

    public override async Task HandleAsync(ImportContactsCommand req, CancellationToken ct)
    {
        if (req.Records == null || req.Records.Length == 0)
        {
            await HttpContext.Response.SendAsync(new ImportResultDto(0, new List<string> { "Request payload cannot be empty." }), 400, cancellation: ct);
            return;
        }

        var result = await _mediator.Send(req, ct);
        await HttpContext.Response.SendAsync(result, cancellation: ct);
    }
}
