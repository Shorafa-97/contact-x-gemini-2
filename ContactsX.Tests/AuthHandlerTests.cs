using ContactsX.Application.DTOs.Auth;
using ContactsX.Application.Features.Auth.Commands;
using ContactsX.Application.Features.Auth.Handlers;
using ContactsX.Application.Interfaces.Repositories;
using ContactsX.Domain.Entities;
using FluentAssertions;
using Moq;
using System.Linq.Expressions;
using Xunit;

namespace ContactsX.Tests;

public class AuthHandlerTests
{
    private readonly Mock<IRepository<User>> _userRepositoryMock;

    public AuthHandlerTests()
    {
        _userRepositoryMock = new Mock<IRepository<User>>();
    }

    [Fact]
    public async Task LoginCommandHandler_ShouldReturnResponse_WhenCredentialsAreValid()
    {
        // Arrange
        var user = new User { UserName = "testuser", PasswordHash = "password123" };
        var users = new List<User> { user };
        _userRepositoryMock.Setup(r => r.FindAsync(It.IsAny<Expression<Func<User, bool>>>()))
            .ReturnsAsync(users);

        var handler = new LoginCommandHandler(_userRepositoryMock.Object);
        var request = new LoginRequestDto("testuser", "password123");

        // Act
        var result = await handler.Handle(new LoginCommand(request), CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result!.UserName.Should().Be("testuser");
        result.Token.Should().Be("mock-jwt-token");
    }

    [Fact]
    public async Task LoginCommandHandler_ShouldReturnNull_WhenUserNotFound()
    {
        // Arrange
        _userRepositoryMock.Setup(r => r.FindAsync(It.IsAny<Expression<Func<User, bool>>>()))
            .ReturnsAsync(new List<User>());

        var handler = new LoginCommandHandler(_userRepositoryMock.Object);
        var request = new LoginRequestDto("nonexistent", "password");

        // Act
        var result = await handler.Handle(new LoginCommand(request), CancellationToken.None);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task LoginCommandHandler_ShouldReturnNull_WhenPasswordIsIncorrect()
    {
        // Arrange
        var user = new User { UserName = "testuser", PasswordHash = "correctpassword" };
        var users = new List<User> { user };
        _userRepositoryMock.Setup(r => r.FindAsync(It.IsAny<Expression<Func<User, bool>>>()))
            .ReturnsAsync(users);

        var handler = new LoginCommandHandler(_userRepositoryMock.Object);
        var request = new LoginRequestDto("testuser", "wrongpassword");

        // Act
        var result = await handler.Handle(new LoginCommand(request), CancellationToken.None);

        // Assert
        result.Should().BeNull();
    }
}
