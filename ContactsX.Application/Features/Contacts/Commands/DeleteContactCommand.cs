using MediatR;

namespace ContactsX.Application.Features.Contacts.Commands;

public record DeleteContactCommand(Guid Id) : IRequest<bool>;
