using Fintrack.Ledger.Application.Commands.DeleteMovement;
using Fintrack.Ledger.Application.Interfaces;
using Fintrack.Ledger.Application.UnitTests.TestHelpers.Builders;
using Fintrack.Ledger.Domain.Entities;
using Fintrack.Ledger.Domain.Interfaces;
using Fintrack.Ledger.Domain.UnitTests.TestHelpers.Builders;

namespace Fintrack.Ledger.Application.UnitTests.Commands.DeleteMovement;

public class DeleteMovementCommandHandlerTests
{
    private readonly Mock<IMovementRepository> _movementRepositoryMock;
    private readonly Mock<ILedgerUnitOfWork> _unitOfWorkMock;
    private readonly DeleteMovementCommandHandler _handler;

    public DeleteMovementCommandHandlerTests()
    {
        _movementRepositoryMock = new Mock<IMovementRepository>();
        _unitOfWorkMock = new Mock<ILedgerUnitOfWork>();
        _handler = new DeleteMovementCommandHandler(
            _movementRepositoryMock.Object,
            _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task HandleAsync_WhenMovementExists_ThenRemovesAndReturnsSuccess()
    {
        // Arrange
        var command = new DeleteMovementCommandBuilder().Build();
        var movement = new MovementBuilder().Build();

        _movementRepositoryMock
            .Setup(r => r.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(movement);

        // Act
        var result = await _handler.HandleAsync(command);

        // Assert
        result.IsSuccess.ShouldBeTrue();

        _movementRepositoryMock.Verify(r =>
            r.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()), Times.Once);

        _movementRepositoryMock.Verify(r =>
            r.RemoveAsync(movement, It.IsAny<CancellationToken>()), Times.Once);

        _unitOfWorkMock.Verify(u =>
            u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

        _movementRepositoryMock.VerifyNoOtherCalls();
        _unitOfWorkMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task HandleAsync_WhenMovementNotFound_ThenReturnsSuccess()
    {
        // Arrange
        var command = new DeleteMovementCommandBuilder().Build();

        _movementRepositoryMock
            .Setup(r => r.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Movement?)null);

        // Act
        var result = await _handler.HandleAsync(command);

        // Assert

        result.IsSuccess.ShouldBeTrue();

        _movementRepositoryMock.Verify(r =>
            r.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()), Times.Once);

        _movementRepositoryMock.VerifyNoOtherCalls();
        _unitOfWorkMock.VerifyNoOtherCalls();
    }
}
