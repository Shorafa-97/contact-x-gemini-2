using ContactsX.Application.DTOs.Entity;
using MediatR;

namespace ContactsX.Application.Features.Entities.Commands;

public record CreateEntityCommand(CreateEntityDto EntityDto) : IRequest<Guid>;
