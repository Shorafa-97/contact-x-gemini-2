using MediatR;
using ContactsX.Application.Features.Duplicates.Commands;

namespace ContactsX.Application.Features.Duplicates.Handlers;

public class DetectDuplicatesHandler : IRequestHandler<DetectDuplicatesCommand>
{
    public Task Handle(DetectDuplicatesCommand request, CancellationToken cancellationToken)
    {
        // Automated detection logic is for a future phase.
        return Task.CompletedTask;
    }
}
