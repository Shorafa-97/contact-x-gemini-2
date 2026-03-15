using ContactsX.Application.DTOs.Relation;
using MediatR;

namespace ContactsX.Application.Features.Contacts.Queries;

public record GetContactRelationsQuery(Guid ContactId) : IRequest<IEnumerable<RelationWithEntityDto>>;
