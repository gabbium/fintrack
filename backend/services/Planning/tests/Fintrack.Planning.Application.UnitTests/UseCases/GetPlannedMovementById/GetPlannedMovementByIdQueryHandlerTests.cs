using Fintrack.Planning.Application.UnitTests.TestHelpers.Builders;
using Fintrack.Planning.Application.UseCases.GetPlannedMovementById;
using Fintrack.Planning.Domain.PlannedMovementAggregate;

namespace Fintrack.Planning.Application.UnitTests.UseCases.GetPlannedMovementById;

public class GetPlannedMovementByIdQueryHandlerTests
{
    private readonly Mock<IPlannedMovementRepository> _plannedMovementRepositoryMock;
    private readonly GetPlannedMovementByIdQueryHandler _handler;

    public GetPlannedMovementByIdQueryHandlerTests()
    {
        _plannedMovementRepositoryMock = new Mock<IPlannedMovementRepository>();
        _handler = new GetPlannedMovementByIdQueryHandler(
            _plannedMovementRepositoryMock.Object);
    }

    [Fact]
    public async Task HandleAsync_WhenPlannedMovementExists_ThenReturnsSuccess()
    {
        // Arrange
        var query = new GetPlannedMovementByIdQueryBuilder().Build();
        var plannedMovement = new PlannedMovementBuilder().Build();

        _plannedMovementRepositoryMock
            .Setup(r => r.GetByIdAsync(query.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(plannedMovement);

        // Act
        var result = await _handler.HandleAsync(query);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldNotBeNull();

        _plannedMovementRepositoryMock.Verify(r =>
            r.GetByIdAsync(query.Id, It.IsAny<CancellationToken>()), Times.Once);

        _plannedMovementRepositoryMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task HandleAsync_WhenPlannedMovementNotFound_ThenReturnsNotFoundError()
    {
        // Arrange
        var query = new GetPlannedMovementByIdQueryBuilder().Build();

        _plannedMovementRepositoryMock
            .Setup(r => r.GetByIdAsync(query.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync((PlannedMovement?)null);

        // Act
        var result = await _handler.HandleAsync(query);

        // Assert
        result.IsSuccess.ShouldBeFalse();
        result.Error.ShouldNotBeNull();
        result.Error.Type.ShouldBe(ErrorType.NotFound);
        result.Error.Description.ShouldBe("Planned movement was not found.");

        _plannedMovementRepositoryMock.Verify(r =>
            r.GetByIdAsync(query.Id, It.IsAny<CancellationToken>()), Times.Once);

        _plannedMovementRepositoryMock.VerifyNoOtherCalls();
    }
}

