using Fintrack.Planning.Application.UnitTests.TestHelpers.Builders;
using Fintrack.Planning.Application.UseCases.CancelPlannedMovement;
using Fintrack.Planning.Domain.PlannedMovementAggregate;

namespace Fintrack.Planning.Application.UnitTests.UseCases.CancelPlannedMovement;

public class CancelPlannedMovementCommandHandlerTests
{
    private readonly Mock<IPlannedMovementRepository> _plannedMovementRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly CancelPlannedMovementCommandHandler _handler;

    public CancelPlannedMovementCommandHandlerTests()
    {
        _plannedMovementRepositoryMock = new Mock<IPlannedMovementRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new CancelPlannedMovementCommandHandler(
            _plannedMovementRepositoryMock.Object,
            _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task HandleAsync_WhenPlannedMovementIsActive_ThenCancelsAndReturnsSuccess()
    {
        // Arrange
        var command = new CancelPlannedMovementCommandBuilder().Build();
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
            r.UpdateAsync(plannedMovement, It.IsAny<CancellationToken>()), Times.Once);

        _unitOfWorkMock.Verify(u =>
            u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

        _plannedMovementRepositoryMock.VerifyNoOtherCalls();
        _unitOfWorkMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task HandleAsync_WhenPlannedMovementExistsButNotActive_ThenReturnsBusinessError()
    {
        // Arrange
        var command = new CancelPlannedMovementCommandBuilder().Build();
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
        result.Error.Description.ShouldBe("Planned movement must be active to be canceled.");

        _plannedMovementRepositoryMock.Verify(r =>
            r.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()), Times.Once);

        _plannedMovementRepositoryMock.VerifyNoOtherCalls();
        _unitOfWorkMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task HandleAsync_WhenPlannedMovementNotFound_ThenReturnsNotFoundError()
    {
        // Arrange
        var command = new CancelPlannedMovementCommandBuilder().Build();

        _plannedMovementRepositoryMock
            .Setup(r => r.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync((PlannedMovement?)null);

        // Act
        var result = await _handler.HandleAsync(command);

        // Assert
        result.IsSuccess.ShouldBeFalse();
        result.Error.ShouldNotBeNull();
        result.Error.Type.ShouldBe(ErrorType.NotFound);
        result.Error.Description.ShouldBe("Planned movement was not found.");

        _plannedMovementRepositoryMock.Verify(r =>
            r.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()), Times.Once);

        _plannedMovementRepositoryMock.VerifyNoOtherCalls();
        _unitOfWorkMock.VerifyNoOtherCalls();
    }
}
