using MediatR;

namespace ContactsX.Application.Features.Duplicates.Commands;

public record DismissDuplicateCommand(Guid Id) : IRequest<bool>;
