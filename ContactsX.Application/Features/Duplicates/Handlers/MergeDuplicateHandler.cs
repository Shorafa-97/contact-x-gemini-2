using MediatR;
using ContactsX.Application.Features.Duplicates.Commands;
using ContactsX.Application.Interfaces.Repositories;
using ContactsX.Domain.Entities;

namespace ContactsX.Application.Features.Duplicates.Handlers;

public class MergeDuplicateHandler : IRequestHandler<MergeDuplicateCommand, bool>
{
    private readonly IRepository<DuplicateCandidate> _repository;

    public MergeDuplicateHandler(IRepository<DuplicateCandidate> repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(MergeDuplicateCommand request, CancellationToken cancellationToken)
    {
        var candidate = await _repository.GetByIdAsync(request.Id);
        if (candidate == null) return false;

        candidate.Status = "merged";
        candidate.ResolvedAt = DateTime.UtcNow;
        // In a real scenario, we might set ResolvedBy from the current user.

        _repository.Update(candidate);
        await _repository.SaveChangesAsync();
        return true;
    }
}
