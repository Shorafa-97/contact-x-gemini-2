using MediatR;
using ContactsX.Application.Features.Duplicates.Queries;
using ContactsX.Application.DTOs.Duplicate;
using ContactsX.Application.Interfaces.Repositories;
using ContactsX.Domain.Entities;
using System.Text.Json;

namespace ContactsX.Application.Features.Duplicates.Handlers;

public class GetDuplicatesHandler : IRequestHandler<GetDuplicatesQuery, IEnumerable<DuplicateCandidateDto>>
{
    private readonly IRepository<DuplicateCandidate> _repository;

    public GetDuplicatesHandler(IRepository<DuplicateCandidate> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<DuplicateCandidateDto>> Handle(GetDuplicatesQuery request, CancellationToken cancellationToken)
    {
        var candidates = await _repository.GetAllAsync();
        return candidates.Select(c => new DuplicateCandidateDto(
            c.Id,
            c.EntityType,
            c.Record1Id,
            c.Record2Id,
            c.MatchScore,
            string.IsNullOrEmpty(c.MatchReasons) ? null : JsonSerializer.Deserialize<List<string>>(c.MatchReasons),
            c.Status,
            c.CreatedAt,
            c.ResolvedAt,
            c.ResolvedBy,
            c.Record1Snapshot,
            c.Record2Snapshot));
    }
}
