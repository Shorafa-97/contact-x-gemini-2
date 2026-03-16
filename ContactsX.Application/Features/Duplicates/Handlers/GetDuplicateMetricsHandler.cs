using MediatR;
using ContactsX.Application.Features.Duplicates.Queries;
using ContactsX.Application.DTOs.Duplicate;
using ContactsX.Application.Interfaces.Repositories;
using ContactsX.Domain.Entities;

namespace ContactsX.Application.Features.Duplicates.Handlers;

public class GetDuplicateMetricsHandler : IRequestHandler<GetDuplicateMetricsQuery, DuplicateMetricsDto>
{
    private readonly IRepository<DuplicateCandidate> _repository;

    public GetDuplicateMetricsHandler(IRepository<DuplicateCandidate> repository)
    {
        _repository = repository;
    }

    public async Task<DuplicateMetricsDto> Handle(GetDuplicateMetricsQuery request, CancellationToken cancellationToken)
    {
        var candidates = await _repository.GetAllAsync();
        var total = candidates.Count();
        var high = candidates.Count(c => c.MatchScore >= 80);
        var medium = candidates.Count(c => c.MatchScore >= 50 && c.MatchScore < 80);
        var low = candidates.Count(c => c.MatchScore < 50);
        var pending = candidates.Count(c => c.Status == "pending");
        var resolved = candidates.Count(c => c.Status != "pending");
        var rate = total == 0 ? 0 : (resolved * 100) / total;

        return new DuplicateMetricsDto(total, high, medium, low, pending, resolved, rate);
    }
}
