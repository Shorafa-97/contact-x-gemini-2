using MediatR;
using ContactsX.Application.DTOs.Import;
using System.Text.Json;

namespace ContactsX.Application.Features.Import.Commands;

public record ImportContactsCommand(JsonElement[] Records) : IRequest<ImportResultDto>;

public record ImportEntitiesCommand(JsonElement[] Records) : IRequest<ImportResultDto>;
