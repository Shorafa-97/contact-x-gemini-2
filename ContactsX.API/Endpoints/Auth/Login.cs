using FastEndpoints;
using MediatR;
using ContactsX.Application.DTOs.Auth;
using ContactsX.Application.Features.Auth.Commands;

namespace ContactsX.API.Endpoints.Auth;

public class LoginEndpoint : Endpoint<LoginRequestDto, LoginResponseDto>
{
    private readonly IMediator _mediator;

    public LoginEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Post("login");
        Group<AuthGroup>();
    }

    public override async Task HandleAsync(LoginRequestDto req, CancellationToken ct)
    {
        var result = await _mediator.Send(new LoginCommand(req), ct);

        if (result == null)
        {
            await HttpContext.Response.SendAsync(new { message = "Unauthorized" }, 401, cancellation: ct);
            return;
        }

        await HttpContext.Response.SendAsync(result, cancellation: ct);
    }
}
