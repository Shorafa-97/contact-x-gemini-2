using FastEndpoints;
using MediatR;
using ContactsX.Application.DTOs.AuditLog;
using ContactsX.Application.Features.AuditLogs.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ContactsX.API.Endpoints.AuditLogs;

public class ListAuditLogsEndpoint : Endpoint<GetAuditLogsQuery, IEnumerable<AuditLogDto>>
{
    private readonly IMediator _mediator;

    public ListAuditLogsEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get("");
        Group<AuditLogGroup>();
    }

    public override async Task HandleAsync(GetAuditLogsQuery req, CancellationToken ct)
    {
        var result = await _mediator.Send(req, ct);
        await HttpContext.Response.SendAsync(result, cancellation: ct);
    }
}
