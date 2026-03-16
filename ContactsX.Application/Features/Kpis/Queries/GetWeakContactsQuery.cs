using MediatR;
using ContactsX.Application.DTOs.Contact;

namespace ContactsX.Application.Features.Kpis.Queries;

public record GetWeakContactsQuery(int Limit = 50) : IRequest<IEnumerable<ContactDto>>;
