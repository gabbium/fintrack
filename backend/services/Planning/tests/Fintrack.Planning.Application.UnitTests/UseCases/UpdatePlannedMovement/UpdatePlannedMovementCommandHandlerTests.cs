using Fintrack.Planning.Application.UnitTests.TestHelpers.Builders;
using Fintrack.Planning.Application.UseCases.UpdatePlannedMovement;
using Fintrack.Planning.Domain.PlannedMovementAggregate;

namespace Fintrack.Planning.Application.UnitTests.UseCases.UpdatePlannedMovement;

public class UpdatePlannedMovementCommandHandlerTests
{
    private readonly Mock<IPlannedMovementRepository> _plannedMovementRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly UpdatePlannedMovementCommandHandler _handler;

    public UpdatePlannedMovementCommandHandlerTests()
    {
        _plannedMovementRepositoryMock = new Mock<IPlannedMovementRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new UpdatePlannedMovementCommandHandler(
            _plannedMovementRepositoryMock.Object,
            _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task HandleAsync_WhenPlannedMovementIsActive_ThenUpdatesAndReturnsSuccess()
    {
        // Arrange
        var command = new UpdatePlannedMovementCommandBuilder().Build();
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
        result.Value.ShouldNotBeNull();

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
        var command = new UpdatePlannedMovementCommandBuilder().Build();
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
        result.Error.Description.ShouldBe("Planned movement must be active to be modified.");

        _plannedMovementRepositoryMock.Verify(r =>
            r.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()), Times.Once);

        _plannedMovementRepositoryMock.VerifyNoOtherCalls();
        _unitOfWorkMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task HandleAsync_WhenPlannedMovementNotFound_ThenReturnsNotFoundError()
    {
        // Arrange
        var command = new UpdatePlannedMovementCommandBuilder().Build();

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
