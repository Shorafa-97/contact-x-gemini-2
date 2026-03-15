using ContactsX.Application.DTOs.Contact;
using MediatR;

namespace ContactsX.Application.Features.Contacts.Commands;

public record UpdateContactCommand(Guid Id, UpdateContactDto ContactDto) : IRequest<bool>;
