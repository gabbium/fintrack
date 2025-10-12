using Fintrack.Planning.Application.Models;
using Fintrack.Planning.Application.Queries.ListPlannedMovements;
using Fintrack.Planning.Application.UnitTests.TestSupport.Builders;

namespace Fintrack.Planning.Application.UnitTests.Queries.ListPlannedMovements;

public class ListPlannedMovementsQueryHandlerTests
{
    private readonly Mock<IListPlannedMovementsQueryService> _listPlannedMovementsQueryServiceMock;
    private readonly ListPlannedMovementsQueryHandler _handler;

    public ListPlannedMovementsQueryHandlerTests()
    {
        _listPlannedMovementsQueryServiceMock = new Mock<IListPlannedMovementsQueryService>();
        _handler = new ListPlannedMovementsQueryHandler(
            _listPlannedMovementsQueryServiceMock.Object);
    }

    [Fact]
    public async Task HandleAsync_ReturnsSuccess()
    {
        // Arrange
        var query = new ListPlannedMovementsQueryBuilder().Build();

        var paginatedList = new PaginatedList<PlannedMovementDto>(
            [],
            100,
            query.PageNumber,
            query.PageSize);

        _listPlannedMovementsQueryServiceMock
            .Setup(s => s.ListAsync(query, It.IsAny<CancellationToken>()))
            .ReturnsAsync(paginatedList);

        // Act
        var result = await _handler.HandleAsync(query);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldBe(paginatedList);

        _listPlannedMovementsQueryServiceMock.Verify(s =>
            s.ListAsync(query, It.IsAny<CancellationToken>()), Times.Once);

        _listPlannedMovementsQueryServiceMock.VerifyNoOtherCalls();
    }
}
