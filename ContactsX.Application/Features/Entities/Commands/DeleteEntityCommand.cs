using MediatR;

namespace ContactsX.Application.Features.Entities.Commands;

public record DeleteEntityCommand(Guid Id) : IRequest<bool>;
