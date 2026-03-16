using MediatR;
using ContactsX.Application.DTOs.Entity;

namespace ContactsX.Application.Features.Kpis.Queries;

public record GetOrphanEntitiesQuery() : IRequest<IEnumerable<EntityDto>>;
