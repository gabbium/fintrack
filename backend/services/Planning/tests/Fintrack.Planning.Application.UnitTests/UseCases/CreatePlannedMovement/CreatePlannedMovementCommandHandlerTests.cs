using Fintrack.Planning.Application.Interfaces;
using Fintrack.Planning.Application.UnitTests.TestHelpers.Builders;
using Fintrack.Planning.Application.UseCases.CreatePlannedMovement;
using Fintrack.Planning.Domain.PlannedMovementAggregate;

namespace Fintrack.Planning.Application.UnitTests.UseCases.CreatePlannedMovement;

public class CreatePlannedMovementCommandHandlerTests
{
    private readonly Mock<IIdentityService> _identityService;
    private readonly Mock<IPlannedMovementRepository> _plannedMovementRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly CreatePlannedMovementCommandHandler _handler;

    public CreatePlannedMovementCommandHandlerTests()
    {
        _identityService = new Mock<IIdentityService>();
        _plannedMovementRepositoryMock = new Mock<IPlannedMovementRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new CreatePlannedMovementCommandHandler(
            _identityService.Object,
            _plannedMovementRepositoryMock.Object,
            _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task HandleAsync_WhenValid_ThenPersistsAndReturnsSuccess()
    {
        // Arrange
        var command = new CreatePlannedMovementCommandBuilder().Build();

        _identityService
            .Setup(i => i.GetUserIdentity())
            .Returns(Guid.NewGuid());

        // Act
        var result = await _handler.HandleAsync(command);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldNotBeNull();

        _identityService.Verify(i => i.GetUserIdentity(), Times.Once());

        _plannedMovementRepositoryMock.Verify(r =>
            r.AddAsync(It.IsAny<PlannedMovement>(), It.IsAny<CancellationToken>()), Times.Once);

        _unitOfWorkMock.Verify(u =>
            u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

        _identityService.VerifyNoOtherCalls();
        _plannedMovementRepositoryMock.VerifyNoOtherCalls();
        _unitOfWorkMock.VerifyNoOtherCalls();
    }
}
