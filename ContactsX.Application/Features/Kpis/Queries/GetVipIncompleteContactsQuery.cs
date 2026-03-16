using MediatR;
using ContactsX.Application.DTOs.Contact;

namespace ContactsX.Application.Features.Kpis.Queries;

public record GetVipIncompleteContactsQuery() : IRequest<IEnumerable<ContactDto>>;
