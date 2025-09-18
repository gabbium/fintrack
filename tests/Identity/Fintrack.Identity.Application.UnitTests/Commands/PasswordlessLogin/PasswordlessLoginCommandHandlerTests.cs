using CleanArch;
using Fintrack.Identity.Application.Commands.PasswordlessLogin;
using Fintrack.Identity.Application.Interfaces;
using Fintrack.Identity.Application.UnitTests.TestHelpers.Builders;
using Fintrack.Identity.Domain.Entities;
using Fintrack.Identity.Domain.Interfaces;
using Fintrack.Identity.Domain.UnitTests.TestHelpers.Builders;
using Moq;

namespace Fintrack.Identity.Application.UnitTests.Commands.PasswordlessLogin;

public class PasswordlessLoginCommandHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IJwtService> _jwtServiceMock;
    private readonly PasswordlessLoginCommandHandler _handler;

    public PasswordlessLoginCommandHandlerTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _jwtServiceMock = new Mock<IJwtService>();

        _handler = new PasswordlessLoginCommandHandler(
            _userRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _jwtServiceMock.Object);
    }

    [Fact]
    public async Task HandleAsync_WhenUserExists_ThenReturnsSuccess()
    {
        // Arrange
        var user = new UserBuilder().Build();

        var command = new PasswordlessLoginCommandBuilder()
            .WithEmail(user.Email)
            .Build();

        var expectedToken = "jwt-token-123";

        _userRepositoryMock
            .Setup(r => r.GetByEmailAsync(command.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        _jwtServiceMock
            .Setup(t => t.CreateAccessToken(user))
            .Returns(expectedToken);

        // Act
        var result = await _handler.HandleAsync(command);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldNotBeNull();

        _userRepositoryMock.Verify(r =>
            r.GetByEmailAsync(command.Email, It.IsAny<CancellationToken>()), Times.Once);

        _jwtServiceMock.Verify(t =>
            t.CreateAccessToken(user), Times.Once);

        _userRepositoryMock.VerifyNoOtherCalls();
        _unitOfWorkMock.VerifyNoOtherCalls();
        _jwtServiceMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task HandleAsync_WhenUserDoesNotExist_ThenCreatesUserAndReturnsSuccess()
    {
        // Arrange
        var command = new PasswordlessLoginCommandBuilder().Build();

        var expectedToken = "jwt-token-123";

        _userRepositoryMock
            .Setup(r => r.GetByEmailAsync(command.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync((User?)null);

        _jwtServiceMock
            .Setup(t => t.CreateAccessToken(It.IsAny<User>()))
            .Returns(expectedToken);

        // Act
        var result = await _handler.HandleAsync(command);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldNotBeNull();

        _userRepositoryMock.Verify(r =>
            r.GetByEmailAsync(command.Email, It.IsAny<CancellationToken>()), Times.Once);

        _userRepositoryMock.Verify(r =>
            r.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once);

        _unitOfWorkMock.Verify(u =>
            u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

        _jwtServiceMock.Verify(t =>
            t.CreateAccessToken(It.IsAny<User>()), Times.Once);

        _userRepositoryMock.VerifyNoOtherCalls();
        _unitOfWorkMock.VerifyNoOtherCalls();
        _jwtServiceMock.VerifyNoOtherCalls();
    }
}
