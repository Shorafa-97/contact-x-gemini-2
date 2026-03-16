using MediatR;
using ContactsX.Application.Features.Duplicates.Commands;
using ContactsX.Application.Interfaces.Repositories;
using ContactsX.Domain.Entities;

namespace ContactsX.Application.Features.Duplicates.Handlers;

public class DismissDuplicateHandler : IRequestHandler<DismissDuplicateCommand, bool>
{
    private readonly IRepository<DuplicateCandidate> _repository;

    public DismissDuplicateHandler(IRepository<DuplicateCandidate> repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DismissDuplicateCommand request, CancellationToken cancellationToken)
    {
        var candidate = await _repository.GetByIdAsync(request.Id);
        if (candidate == null) return false;

        candidate.Status = "dismissed";
        candidate.ResolvedAt = DateTime.UtcNow;

        _repository.Update(candidate);
        await _repository.SaveChangesAsync();
        return true;
    }
}
