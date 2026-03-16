using MediatR;

namespace ContactsX.Application.Features.Duplicates.Commands;

public record DetectDuplicatesCommand() : IRequest;
