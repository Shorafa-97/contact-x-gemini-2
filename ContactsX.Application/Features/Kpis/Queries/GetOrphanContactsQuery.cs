using MediatR;
using ContactsX.Application.DTOs.Contact;

namespace ContactsX.Application.Features.Kpis.Queries;

public record GetOrphanContactsQuery() : IRequest<IEnumerable<ContactDto>>;
