using ContactsX.Application.DTOs.Entity;
using MediatR;

namespace ContactsX.Application.Features.Entities.Queries;

public record GetEntityChildrenQuery(Guid Id) : IRequest<IEnumerable<EntityDto>?>;
