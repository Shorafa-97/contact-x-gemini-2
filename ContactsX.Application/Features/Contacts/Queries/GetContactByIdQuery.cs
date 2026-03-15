using ContactsX.Application.DTOs.Contact;
using MediatR;

namespace ContactsX.Application.Features.Contacts.Queries;

public record GetContactByIdQuery(Guid Id) : IRequest<ContactDto?>;
