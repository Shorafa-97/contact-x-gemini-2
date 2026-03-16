using MediatR;
using ContactsX.Application.DTOs.Duplicate;

namespace ContactsX.Application.Features.Duplicates.Commands;

public record MergeDuplicateCommand(Guid Id, MergeRequest Request) : IRequest<bool>;
