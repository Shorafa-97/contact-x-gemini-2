using ContactsX.Application.DTOs.Entity;
using MediatR;

namespace ContactsX.Application.Features.Entities.Queries;

public record GetEntityContactsQuery(Guid Id) : IRequest<IEnumerable<RelationWithContactDto>?>;
