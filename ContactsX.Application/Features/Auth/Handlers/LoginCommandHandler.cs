using MediatR;
using ContactsX.Application.DTOs.Auth;
using ContactsX.Application.Features.Auth.Commands;
using ContactsX.Application.Interfaces.Repositories;

using ContactsX.Domain.Entities;

namespace ContactsX.Application.Features.Auth.Handlers;

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponseDto?>
{
    private readonly IRepository<User> _userRepository;

    public LoginCommandHandler(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<LoginResponseDto?> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        var users = await _userRepository.FindAsync(u => u.UserName == command.Request.UserName);
        var user = users.FirstOrDefault();

        if (user == null)
        {
            return null;
        }

        // Simplified password check for this stage
        if (user.PasswordHash != command.Request.Password)
        {
            return null;
        }

        // Return a mock token for now
        return new LoginResponseDto("mock-jwt-token", user.UserName);
    }
}
