using ContactsX.Application.DTOs.Contact;
using MediatR;

namespace ContactsX.Application.Features.Contacts.Commands;

public record CreateContactCommand(CreateContactDto ContactDto) : IRequest<Guid>;
