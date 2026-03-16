using MediatR;
using ContactsX.Application.DTOs.Entity;

namespace ContactsX.Application.Features.Kpis.Queries;

public record GetWeakEntitiesQuery(int Limit = 50) : IRequest<IEnumerable<EntityDto>>;
