using ContactsX.Application.DTOs.Relation;
using MediatR;

namespace ContactsX.Application.Features.Contacts.Commands;

public record CreateRelationCommand(Guid ContactId, CreateRelationDto RelationDto) : IRequest<RelationDto?>;
