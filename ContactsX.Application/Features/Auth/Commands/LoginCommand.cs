using MediatR;
using ContactsX.Application.DTOs.Auth;

namespace ContactsX.Application.Features.Auth.Commands;

public record LoginCommand(LoginRequestDto Request) : IRequest<LoginResponseDto?>;
