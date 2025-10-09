using Fintrack.Planning.Application.UnitTests.TestHelpers.Builders;
using Fintrack.Planning.Application.UseCases.RealizePlannedMovement;
using Fintrack.Planning.Domain.PlannedMovementAggregate;

namespace Fintrack.Planning.Application.UnitTests.UseCases.RealizePlannedMovement;

public class RealizePlannedMovementCommandHandlerTests
{
    private readonly Mock<IPlannedMovementRepository> _plannedMovementRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly RealizePlannedMovementCommandHandler _handler;

    public RealizePlannedMovementCommandHandlerTests()
    {
        _plannedMovementRepositoryMock = new Mock<IPlannedMovementRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new RealizePlannedMovementCommandHandler(
            _plannedMovementRepositoryMock.Object,
            _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task HandleAsync_WhenPlannedMovementIsActive_ThenRealizesAndReturnsSuccess()
    {
        // Arrange
        var command = new RealizePlannedMovementCommandBuilder().Build();
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
        var command = new RealizePlannedMovementCommandBuilder().Build();
        var plannedMovement = new PlannedMovementBuilder()
            .WithStatus(PlannedMovementStatus.Canceled)
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
        result.Error.Description.ShouldBe("Planned movement must be active to be realized.");

        _plannedMovementRepositoryMock.Verify(r =>
            r.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()), Times.Once);

        _plannedMovementRepositoryMock.VerifyNoOtherCalls();
        _unitOfWorkMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task HandleAsync_WhenPlannedMovementNotFound_ThenReturnsNotFoundError()
    {
        // Arrange
        var command = new RealizePlannedMovementCommandBuilder().Build();

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
