using Fintrack.Ledger.Application.Commands.CreateMovement;
using Fintrack.Ledger.Application.Interfaces;
using Fintrack.Ledger.Application.UnitTests.TestHelpers.Builders;
using Fintrack.Ledger.Domain.Entities;
using Fintrack.Ledger.Domain.Interfaces;

namespace Fintrack.Ledger.Application.UnitTests.Commands.CreateMovement;

public class CreateMovementCommandHandlerTests
{
    private readonly Mock<IUser> _user;
    private readonly Mock<IMovementRepository> _movementRepositoryMock;
    private readonly Mock<ILedgerUnitOfWork> _unitOfWorkMock;
    private readonly CreateMovementCommandHandler _handler;

    public CreateMovementCommandHandlerTests()
    {
        _user = new Mock<IUser>();
        _movementRepositoryMock = new Mock<IMovementRepository>();
        _unitOfWorkMock = new Mock<ILedgerUnitOfWork>();
        _handler = new CreateMovementCommandHandler(
            _user.Object,
            _movementRepositoryMock.Object,
            _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task HandleAsync_WhenValid_ThenPersistsAndReturnsSuccess()
    {
        // Arrange
        var command = new CreateMovementCommandBuilder().Build();

        _user
            .SetupGet(o => o.Id)
            .Returns(Guid.NewGuid());

        // Act
        var result = await _handler.HandleAsync(command);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldNotBeNull();

        _user.Verify(o => o.Id, Times.Once());

        _movementRepositoryMock.Verify(r =>
            r.AddAsync(It.IsAny<Movement>(), It.IsAny<CancellationToken>()), Times.Once);

        _unitOfWorkMock.Verify(u =>
            u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

        _user.VerifyNoOtherCalls();
        _movementRepositoryMock.VerifyNoOtherCalls();
        _unitOfWorkMock.VerifyNoOtherCalls();
    }
}
