using MediatR;
using ContactsX.Application.DTOs.Duplicate;

namespace ContactsX.Application.Features.Duplicates.Queries;

public record GetDuplicatesQuery() : IRequest<IEnumerable<DuplicateCandidateDto>>;
