using FastEndpoints;
using MediatR;
using ContactsX.Application.DTOs.Duplicate;
using ContactsX.Application.Features.Duplicates.Commands;

namespace ContactsX.API.Endpoints.Duplicates;

public class MergeDuplicateEndpoint : Endpoint<MergeRequest, object>
{
    private readonly IMediator _mediator;

    public MergeDuplicateEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Post("{id:guid}/merge");
        Group<DuplicateGroup>();
    }

    public override async Task HandleAsync(MergeRequest req, CancellationToken ct)
    {
        var id = Route<Guid>("id");
        var success = await _mediator.Send(new MergeDuplicateCommand(id, req), ct);

        if (!success)
        {
            await HttpContext.Response.SendNotFoundAsync(ct);
            return;
        }

        await HttpContext.Response.SendOkAsync(ct);
    }
}
