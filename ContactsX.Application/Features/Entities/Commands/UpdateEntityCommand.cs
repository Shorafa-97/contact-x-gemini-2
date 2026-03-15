using ContactsX.Application.DTOs.Entity;
using MediatR;

namespace ContactsX.Application.Features.Entities.Commands;

public record UpdateEntityCommand(Guid Id, UpdateEntityDto EntityDto) : IRequest<bool>;
