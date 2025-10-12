using Fintrack.Planning.Application.Commands.DeletePlannedMovement;
using Fintrack.Planning.Application.UnitTests.TestSupport.Builders;
using Fintrack.Planning.Domain.PlannedMovementAggregate;

namespace Fintrack.Planning.Application.UnitTests.Commands.DeletePlannedMovement;

public class DeletePlannedMovementCommandHandlerTests
{
    private readonly Mock<IPlannedMovementRepository> _plannedMovementRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly DeletePlannedMovementCommandHandler _handler;

    public DeletePlannedMovementCommandHandlerTests()
    {
        _plannedMovementRepositoryMock = new Mock<IPlannedMovementRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new DeletePlannedMovementCommandHandler(
            _plannedMovementRepositoryMock.Object,
            _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task HandleAsync_WhenPlannedMovementIsActive_ThenRemovesAndReturnsSuccess()
    {
        // Arrange
        var command = new DeletePlannedMovementCommandBuilder().Build();
        var plannedMovement = new PlannedMovementBuilder()
            .WithStatus(PlannedMovementStatus.Active)
            .Build();

        _plannedMovementRepositoryMock
            .Setup(r => r.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(plannedMovement);

        // Act
        var result = await _handler.HandleAsync(command);

        // Assert
        result.IsSuccess.ShouldBeTrue();

        _plannedMovementRepositoryMock.Verify(r =>
            r.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()), Times.Once);

        _plannedMovementRepositoryMock.Verify(r =>
            r.RemoveAsync(plannedMovement, It.IsAny<CancellationToken>()), Times.Once);

        _unitOfWorkMock.Verify(u =>
            u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

        _plannedMovementRepositoryMock.VerifyNoOtherCalls();
        _unitOfWorkMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task HandleAsync_WhenPlannedMovementExistsButNotActive_ThenReturnsBusinessError()
    {
        // Arrange
        var command = new DeletePlannedMovementCommandBuilder().Build();
        var plannedMovement = new PlannedMovementBuilder()
            .WithStatus(PlannedMovementStatus.Realized)
            .Build();

        _plannedMovementRepositoryMock
            .Setup(r => r.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(plannedMovement);

        // Act
        var result = await _handler.HandleAsync(command);

        // Assert
        result.IsSuccess.ShouldBeFalse();
        result.Error.ShouldNotBeNull();
        result.Error.Type.ShouldBe(ErrorType.Business);
        result.Error.Description.ShouldBe("Planned movement must be active to be deleted.");

        _plannedMovementRepositoryMock.Verify(r =>
            r.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()), Times.Once);

        _plannedMovementRepositoryMock.VerifyNoOtherCalls();
        _unitOfWorkMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task HandleAsync_WhenPlannedMovementNotFound_ThenReturnsSuccess()
    {
        // Arrange
        var command = new DeletePlannedMovementCommandBuilder().Build();

        _plannedMovementRepositoryMock
            .Setup(r => r.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync((PlannedMovement?)null);

        // Act
        var result = await _handler.HandleAsync(command);

        // Assert
        result.IsSuccess.ShouldBeTrue();

        _plannedMovementRepositoryMock.Verify(r =>
            r.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()), Times.Once);

        _plannedMovementRepositoryMock.VerifyNoOtherCalls();
        _unitOfWorkMock.VerifyNoOtherCalls();
    }
}
