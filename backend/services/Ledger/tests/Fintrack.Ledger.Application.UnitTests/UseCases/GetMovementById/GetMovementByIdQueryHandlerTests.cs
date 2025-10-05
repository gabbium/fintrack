using Fintrack.Ledger.Application.UnitTests.TestHelpers.Builders;
using Fintrack.Ledger.Application.UseCases.GetMovementById;
using Fintrack.Ledger.Domain.MovementAggregate;

namespace Fintrack.Ledger.Application.UnitTests.UseCases.GetMovementById;

public class GetMovementByIdQueryHandlerTests
{
    private readonly Mock<IMovementRepository> _movementRepositoryMock;
    private readonly GetMovementByIdQueryHandler _handler;

    public GetMovementByIdQueryHandlerTests()
    {
        _movementRepositoryMock = new Mock<IMovementRepository>();
        _handler = new GetMovementByIdQueryHandler(
            _movementRepositoryMock.Object);
    }

    [Fact]
    public async Task HandleAsync_WhenMovementExists_ThenReturnsSuccess()
    {
        // Arrange
        var query = new GetMovementByIdQueryBuilder().Build();
        var movement = new MovementBuilder().Build();

        _movementRepositoryMock
            .Setup(r => r.GetByIdAsync(query.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(movement);

        // Act
        var result = await _handler.HandleAsync(query);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldNotBeNull();

        _movementRepositoryMock.Verify(r =>
            r.GetByIdAsync(query.Id, It.IsAny<CancellationToken>()), Times.Once);

        _movementRepositoryMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task HandleAsync_WhenMovementNotFound_ThenReturnsNotFoundError()
    {
        // Arrange
        var query = new GetMovementByIdQueryBuilder().Build();

        _movementRepositoryMock
            .Setup(r => r.GetByIdAsync(query.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Movement?)null);

        // Act
        var result = await _handler.HandleAsync(query);

        // Assert
        result.IsSuccess.ShouldBeFalse();
        result.Error.ShouldNotBeNull();
        result.Error.Type.ShouldBe(ErrorType.NotFound);
        result.Error.Description.ShouldBe("Movement was not found.");

        _movementRepositoryMock.Verify(r =>
            r.GetByIdAsync(query.Id, It.IsAny<CancellationToken>()), Times.Once);

        _movementRepositoryMock.VerifyNoOtherCalls();
    }
}
